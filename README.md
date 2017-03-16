# VendingMachineExample
Simple Vending machine Example with TDD and multi-threaded Tests

IVendingMachine - Vending machine interface
ICardAuthority - Card Authority that stores card information
ICard - Card interface
IAccount - Account as independent entity and link to Card

Important tests
VendingMachine.Tests -    a. If the exception is raised when it is out of stock.
Account.Tests - b. Parallel access to account still generates desirable effect.
Card.Tests - c. Multiple threads accessing the same account (linked cash cards to a account) generates desirable effect.                
                       
                       
