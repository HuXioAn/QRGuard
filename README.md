# QRGuard - an Access Control system for gate using QRCode
------
**Works withOUT Network\RTC on the gate, but with QRCode expiration and security based on cryptology.**

## the origin

This is a project proposed coincidently, when I got a K210 dev board with camera, and we were all fed up with the old, traditional ID card access system.

So we decided to add some functions. Using the K210 board to scan the QRCode requested from the server and displayed on smartphone. This method suits situations like offices and teams.

However, the problems were :
1. the board has no wireless connection on board, and the network won't be reliable though.
2. there's no RTC on board, and I Won't add one.

So we adopted techniques like AES,SHA,BASE64,timestamp to ensure there's expiration of QRCodes users requested, and nobody can create a new valid QRCode, no matter how many valid QRCode he have gotten.

## parts of the project

The whole project consists of several parts in different repos, listed below.

[QRGuard](https://github.com/HuXioAn/QRGuard) : the original implementation of the QRcode generation in C#

[QRGuard-K210](https://github.com/HuXioAn/QRGuard-K210) : micropython code for K210 

[QRGuard-WebApi](https://github.com/HuXioAn/QRGuard-WebApi) : a asp.net application provide minimal restful api for the QRCode

[QRGuard-Blazor](https://github.com/HuXioAn/QRGuard-Blazor) : a blazor web page for users to requesting the QRCode

[QRGuard-LarrkBot](https://github.com/HuXioAn/QRGuard-LarrkBot) : a Python service to provide quick authentication for the web page





