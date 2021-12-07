# RayTracer

## Introduction

This project implements and explores ray tracing based on Kevin Suffern's 2007 book "Ray Tracing from the Ground Up" published by A K Peters, Ltd ("the book"). Kevin was my graphics lecturer when I attended university. His enthusiasm and patience made the topic come alive. Thank you, Kevin!

Ray tracing is the practice of generating images by mathematically firing light rays at observing their interaction, such as reflection or refraction. Ray tracing produces many of the scenes movies and is finding its way into games, based on the work from companies like NVidia.

The goals of this project are to:
1. Educate me (and hopefully others) on ray tracing concepts and execution.
2. Create attractive images using light sources, shading, texturing and meshes.
3. NOT be a commercially viable or competitive product. That would require hardware acceleration and an extensive user interface. Either are potential future goals.

Kevin wrote the code from "Ray Tracing from the Ground Up" in C++ because the processing power of personal computers at the time was limited. C++ emphasized efficiency while providing a native object oriented development environment. I converted the code C# to leverage C#'s relative brevity and the .Net standard library's features like parallelization and platform independence. 

## Structure

The ray tracer consists of the following projects:
1. src/RayTracer: The ray tracing library.
2. src/Viewer: Generate and display ray traced images using "RayTracer". Run this to see the output.
3. test/RayTracer.Test: Automated tests for the ray tracer, written in NUnit.
4. tools/SampleViewer: A visualization for the sample generators.

## Principles

I developed this project according to the following principles, listed in order from the most important to least:

### 1. Code is the User Interface

Developers are the target audience of this project. Therefore, this project is configured directly in code instead of spending development effort on parsing instruction files or a user interface. This keeps the code simple, removing configuration and serialization code and dependencies.

This excludes things traditionally loadex externally or pregenerated like meshes and textures.

### 2. Good Object Oriented Design and Readability

Many parts of a ray tracer are substitutable, such as cameras, materials and geometric objects. Object oriented design helps facilitate this while keeping the code readable. 

The book "Ray Tracing from the Ground Up" already follows an object oriented design. However, some parts of the book can be improved, such as the sample generation code in chapter five. Redesign these sections where needed.

The book favours one letter variable names to align with the mathematics more closely. Instead, use descriptive member and variable names for better readability.

An object oriented design also makes building scenes easier (see the "Code is the User Interface" principle).

### 3. Automated Testing and Visualization

Create automated tests to test each functional area during development, ensuring correctness before use. Testable code tends to be better designed (see the "Good Object Oriented Design" principle) and ensures the API is fit for purpose (see "Code is the User Interface").

Where automated tests are not possible, e.g. for randomness, create tools to visualize output or otherwise help demonstrate correctness. A good example is sample generation (see the "SamplerViewer" project). 

### 4. Fast Execution

Modern personal computers are significantly faster than when Kevin authored the book. However, execution speed is still a factor for large, complex scenes. Therefore, leverage parallel execution and similar capabilities where possible.

Where execution speed conflicts with good design, optimize for good design. The execution speed differences are likely minor, and the potential readability loss is significant (see the "Good Object Oriented Design and Readability" principle).

### 5. Immutable Objects

Initialize objects' state at construction, then prevent subsequent modification. This pattern ensures objects are never in an invalid state. As in functional programming, immutable objects are innately parallelizable (see the "Fast Execution" principle).

Arguably already covered by the "Fast Execution" and "Good Object Oriented Design and Readability" principles, this is significant enough to be called out separately.

### 6. Follow the Book

Base the development order and code structure on the C++ code from "Ray Tracing from the Ground Up". This practice makes the book easier to follow. The exercises and illustrations help test and verify the code is correct.

# License

See [LICENSE](LICENSE) for the license.