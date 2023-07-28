# Pizzaaa

To start the application go to the directory src\Pizzaaa.UI.Blazor and run this command.
dotnet run


To install playwright go to test\Pizzaaa.Playwright and run:
bin/Debug/net7.0/playwright.ps1 install

for auto-creating playwright tests, run:
bin/Debug/net7.0/playwright.ps1 codegen http://localhost:5254/pizza

to see screenshots of the test, run:
bin/Debug/net7.0/playwright.ps1 show-trace <nametracezip>
if it doesn't work, you can use https://trace.playwright.dev/

You need to have the application running to be able to test with playwright
