#include "pch.h"
#include <SFML/Graphics.hpp>

const int WIDTH = 800;
const int HEIGHT = 600;
const int MAX_ITERATIONS = 100;

int main()
{
    // Create the window
    sf::RenderWindow window(sf::VideoMode(WIDTH, HEIGHT), "Mandelbrot Set");

    // Create the image
    sf::Image image;
    image.create(WIDTH, HEIGHT, sf::Color::Black);

    // Create the texture and sprite
    sf::Texture texture;
    texture.loadFromImage(image);
    sf::Sprite sprite(texture);

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

        // Draw the Mandelbrot set
        for (int x = 0; x < WIDTH; ++x)
        {
            for (int y = 0; y < HEIGHT; ++y)
            {
                // Map the pixel coordinates to the complex plane
                double real = (double)x / WIDTH * 3.5 - 2.5;
                double imag = (double)y / HEIGHT * 2.0 - 1.0;

                // Initialize the complex number
                double z_real = real;
                double z_imag = imag;

                // Iterate the complex number until it escapes or max iterations are reached
                int iterations = 0;
                while (z_real * z_real + z_imag * z_imag <= 4 && iterations < MAX_ITERATIONS)
                {
                    double z_real_new = z_real * z_real - z_imag * z_imag + real;
                    double z_imag_new = 2 * z_real * z_imag + imag;
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
                // Update the texture and sprite
                
                
            }
            /*_sleep(10);
            texture.update(image);
            window.draw(sprite);
            window.display();*/
        }

        texture.update(image);
        window.draw(sprite);
        window.display();
    }

    return 0;
}