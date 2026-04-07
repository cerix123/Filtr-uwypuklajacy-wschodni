#include "pch.h"
#include <cstdint>
#include <cstring>

extern "C" __declspec(dllexport) void ApplyEmbossEastFilter(
    uint8_t* inputSegmentPtr,          // Wskaźnik na dane wejściowe
    uint8_t* outputSegmentPtr,         // Wskaźnik na dane wyjściowe
    int width,                         // Szerokość obrazu
    int height,                        // Wysokość obrazu
    int stride                         // Rozmiar wiersza w bajtach
) {
    int bytesPerPixel = 3; // Dla formatu RGB
    int processWidth = width - 2;  // Pomijamy krawędzie
    int processHeight = height - 2;

    // Funkcja ograniczająca wartość do zakresu [0, 255]
    auto clamp = [](int value) -> uint8_t {
        return (value < 0) ? 0 : (value > 255 ? 255 : value);
        };

    for (int y = 2; y < processHeight + 1; y++) { // Zaczynamy od 1, kończymy na processHeight + 1
        for (int x = 2; x < processWidth + 1; x++) { // Zaczynamy od 1, kończymy na processWidth + 1
            int pixelOffset = y * stride + x * bytesPerPixel;

            // Sąsiedzi
            const uint8_t* topLeft = &inputSegmentPtr[(y - 1) * stride + (x - 1) * bytesPerPixel];
            const uint8_t* topRight = &inputSegmentPtr[(y - 1) * stride + (x + 1) * bytesPerPixel];
            const uint8_t* middleLeft = &inputSegmentPtr[y * stride + (x - 1) * bytesPerPixel];
            const uint8_t* middle = &inputSegmentPtr[y * stride + x * bytesPerPixel];
            const uint8_t* middleRight = &inputSegmentPtr[y * stride + (x + 1) * bytesPerPixel];
            const uint8_t* bottomLeft = &inputSegmentPtr[(y + 1) * stride + (x - 1) * bytesPerPixel];
            const uint8_t* bottomRight = &inputSegmentPtr[(y + 1) * stride + (x + 1) * bytesPerPixel];

            for (int channel = 0; channel < bytesPerPixel; channel++) {
                int sum =
                    topRight[channel] - topLeft[channel] +
                    middleRight[channel] + middle[channel] - middleLeft[channel] +
                    bottomRight[channel] - bottomLeft[channel];

                // Ogranicz do zakresu [0, 255]
                outputSegmentPtr[pixelOffset + channel] = clamp(sum);
            }
        }
    }
}
