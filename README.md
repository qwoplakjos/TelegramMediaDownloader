# Telegram Bulk Downloader

A Windows Forms application that allows you to authenticate with your Telegram account, browse joined chats and channels, and download media (documents and photos) from them in bulk.

## Features

- **Session Management**: Reuse your Telegram session with a session file (`TelegramSession.ss`), so you don't have to log in every time.
- **API Credentials Storage**: Automatically saves your `api_id`, `api_hash`, and `phone_number` in a credentials file for easy login.
- **Download Media**: Download documents and photos from any chat or channel you're a part of, and save them in the `downloads` folder with unique filenames.
- **User-Friendly UI**: A simple UI to request an authentication code, select chats/channels, and download media files.

## Prerequisites

Before running this application, you need the following:

- **Telegram API Credentials**: You must have a Telegram app registered to obtain the `api_id` and `api_hash`. You can register a new Telegram application [here](https://my.telegram.org/auth).
  
- **.NET Framework**: This project uses **.NET Framework**. Ensure you have it installed.

- **NuGet Packages**:
  - [WTelegramClient](https://www.nuget.org/packages/WTelegramClient/) - A .NET library for interacting with Telegram's API.

## How to Use

1. **Clone the repository**:

    ```bash
    git clone https://github.com/qwoplakjos/TelegramMediaDownloader.git
    cd TelegramMediaDownloader
    ```

    or download the exe:

     - [TelegramBulkDownloader.exe](https://github.com/qwoplakjos/TelegramMediaDownloader/releases/download/1.0.0/TelegramBulkDownloader.exe)

3. **Set up API Credentials**:
   - Obtain your `api_id` and `api_hash` from [my.telegram.org](https://my.telegram.org/auth).
   - Launch the application and input your credentials (`api_id`, `api_hash`, `phone_number`).
   - Save your credentials for future sessions.

4. **Login**:
   - Click the "Request Code" button to receive a verification code via Telegram. Enter the verification code when prompted.

5. **Select Chat or Channel**:
   - After successful authentication, a list of chats and channels you're a part of will be displayed in the UI.
   - Select a chat or channel to download media from.

6. **Download Media**:
   - Click the "Download" button to start downloading media files (documents and photos). Files will be saved in the `downloads` folder.

## App UI:

![image](https://github.com/user-attachments/assets/2592e893-64f4-4ff6-9fe4-0784992e2aa0)
