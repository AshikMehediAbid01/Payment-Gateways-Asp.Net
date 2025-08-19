# Payment-Gateways-Asp.Net

Integration of AamarPay and SSLCommerz payment gateways in an ASP.NET Core application.

## Overview

This project demonstrates how to integrate multiple payment gateways—AamarPay and SSLCommerz—using a strategy pattern in an ASP.NET Core application. It provides a flexible and extensible architecture for handling different payment providers.

## Features

- **AamarPay Integration:** Seamless payment processing with AamarPay.
- **SSLCommerz Integration:** Integration with SSLCommerz for secure online transactions.
- **Strategy Pattern:** Clean and scalable use of the strategy pattern for payment provider abstraction.
- **ASP.NET Core:** Built with ASP.NET Core for modern web development practices.

## Project Structure

- `PaymentStrategyPattern.sln`  
  The solution file for the project.

- `PaymentStrategyPattern/`  
  Contains the main application source code, including payment gateway implementations and strategy pattern logic.

- `.gitignore`  
  Standard git ignore file for .NET projects.

- `.dockerignore`  
  Docker ignore file for containerized builds.

## Getting Started

1. **Clone the repository:**
    ```bash
    git clone https://github.com/AshikMehediAbid01/Payment-Gateways-Asp.Net.git
    ```
2. **Open the solution:**  
   Open `PaymentStrategyPattern.sln` in Visual Studio or your preferred IDE.

3. **Configure payment gateway credentials:**  
   Update your configuration files with your AamarPay and SSLCommerz credentials.

4. **Build and run:**  
   Build the solution and run the project to start integrating payments.

## How It Works

- The application uses the strategy pattern to switch between different payment gateways at runtime.
- Each gateway implementation adheres to a common interface, allowing you to easily add new providers in the future.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is for demonstration and educational purposes and does not currently specify a license.

## Author

[**AshikMehediAbid01**](https://github.com/AshikMehediAbid01)
[**AshikMehediAbid**](https://github.com/AshikMehediAbid)

---
