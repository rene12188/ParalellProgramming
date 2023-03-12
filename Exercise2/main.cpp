#include "pch.h"

#include <complex.h>
#include <iostream>
#include <omp.h>
#include <SFML/Graphics.hpp>

const int WIDTH = 1000;
const int HEIGHT = 1000;
const int MAX_ITERATIONS = 1000;

const double xmin = -10.0;
const double xmax = 5.0;
const double ymin = -5.0;
const double ymax = 5.0;

std::pair<double, double> mapPixel(int x, int y)
{
	double mappedX = xmin + (xmax - xmin) * x / (WIDTH - 1);
	double mappedY = ymin + (ymax - ymin) * y / (HEIGHT - 1);

	return std::make_pair(mappedX, mappedY);
}

int main()
{
	omp_set_num_threads(omp_get_max_threads());
	// Create the window
	sf::RenderWindow window(sf::VideoMode(WIDTH, HEIGHT), "Mandelbrot Set");

	// Create the image
	sf::Image image;
	image.create(WIDTH, HEIGHT, sf::Color::Black);

	// Create the texture and sprite
	sf::Texture texture;
	texture.loadFromImage(image);
	sf::Sprite sprite(texture);

    // Define the initial zoom level and zoom factor
    double zoom = 1;
    const double zoomFactor = 0.99;


    // Main loop
    while (window.isOpen())
    {
        // Handle events
        sf::Event event;
        while (window.pollEvent(event))
        {
            if (event.type == sf::Event::Closed)
            {
                window.close();
            }
        }

        auto start = std::chrono::high_resolution_clock::now();


        const double VIEW_SHIFT = 0.5 * (xmax - xmin);
        // Zoom into the set
        zoom *= zoomFactor;
        double xmin_zoomed = (xmin + VIEW_SHIFT) * zoom;
        double xmax_zoomed = (xmax + VIEW_SHIFT) * zoom;
        double ymin_zoomed = ymin * zoom;
        double ymax_zoomed = ymax * zoom;

#pragma omp parallel
        {
#pragma omp for
            // Draw the Mandelbrot set
            for (int x = 0; x < WIDTH; ++x)
            {
                for (int y = 0; y < HEIGHT; ++y)
                {
                    auto mappedPixel = mapPixel(x, y);
                    double real = mappedPixel.first;
                    double imag = mappedPixel.second;

                    // Map the pixel to the zoomed coordinate system
                    double mappedX = xmin_zoomed + (xmax_zoomed - xmin_zoomed) * x / (WIDTH - 1);
                    double mappedY = ymin_zoomed + (ymax_zoomed - ymin_zoomed) * y / (HEIGHT - 1);

                    // Initialize the complex number
                    double z_real = mappedX;
                    double z_imag = mappedY;

                    // Iterate the complex number until it escapes or max iterations are reached
                    int iterations = 0;
                    while (z_real * z_real + z_imag * z_imag <= 4 && iterations < MAX_ITERATIONS)
                    {
                        double z_real_new = z_real * z_real - z_imag * z_imag + mappedX;
                        double z_imag_new = 2 * z_real * z_imag + mappedY;
                        z_real = z_real_new;
                        z_imag = z_imag_new;
                        ++iterations;
                    }

                    // Set the pixel color based on the number of iterations
                    if (iterations == MAX_ITERATIONS)
                    {
                        image.setPixel(x, y, sf::Color::Black);
                    }
                    else
                    {
                        float t = (float)iterations / MAX_ITERATIONS;
                        int r = (int)(9 * (1 - t) * t * t * t * 255);
                        int g = (int)(15 * (1 - t) * (1 - t) * t * t * 255);
                        int b = (int)(8.5 * (1 - t) * (1 - t) * (1 - t) * t * 255);
                        image.setPixel(x, y, sf::Color(r, g, b));
                    }
                }
            }
            _sleep(5);
        }

        auto end = std::chrono::high_resolution_clock::now();

        auto milliseconds = std::chrono::duration_cast<std::chrono::milliseconds>(end - start);

        // Update the texture and sprite
        texture.update(image);
        window.draw(sprite);
        window.display();

        std::cout << "elapsed milliseconds: " << milliseconds.count() << std::endl;
		//break;
	}

	return 0;
}