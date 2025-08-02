# Minesweeper

This is a classic Minesweeper game built using the ASP.NET MVC framework. The project provides a playable web version of the popular puzzle game with multiple difficulty levels.

## Features

-   **Difficulty Levels:** Choose between Easy, Medium, and Hard board sizes and mine counts. (Coming soon!)
-   **Game Logic:** Implements all core Minesweeper mechanics, including revealing cells, flagging mines, and win/loss conditions.
-   **Mine Counter:** Displays the remaining number of mines. (Timer coming soon as well as competitive leaderboard)

## Technologies Used

-   **ASP.NET MVC:** The core framework for the web application.
-   **C#:** The primary language for the backend game logic and controllers.
-   **JavaScript:** Used for the game interactivity.

## Setup and Installation

To get this project up and running on your local machine, follow these steps:

1.  **Clone the repository:**
    ```
    git clone [https://github.com/LekhivOleh/Minesweeper.git](https://github.com/LekhivOleh/Minesweeper.git)
    ```

2.  **Open in Visual Studio:**
    * Open the solution file (`.sln`) in Visual Studio.

3.  **Run the project:**
    ```
    dotnet run --project src/Minesweeper.API
    ```    or press `F6`.
4. **Enjoy the game:** \
    Open ```http://localhost:5118/``` in browser.

## How to Play
#### If youre already familiar with Minesweeper, you can jump right in. If not, here are the basic rules:

-   Click on a cell to reveal it.
-   If you reveal a mine, the game is over.
-   If you reveal a number, it indicates how many mines are in the surrounding cells.
-   Right-click to place a flag on a cell you suspect contains a mine.
-   Middle-click to place a question-mark on a cell to indicate that you are unsure if it contains a mine.
-   Win the game by revealing all non-mine cells (flagging all mines is not nessesary).
