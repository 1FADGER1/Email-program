# Система обмена электронной почтой (Email обработчик) 📧
Реализованы 2 приложения отправка и получение почтовых писем.

## Как работает
Мы по протоколам IMAP и SMTP автоматически подключаемся к почтовому сервису гугл и можем отправить или прочитать сообщения.
### Отправка 💬
Отправление email происходит в приложении Sender. Мы можем прикрепить к сообщению любой файл и написать текст.
### Чтение
Прочитать сообщение можно с помощью Addressees (или на почте). Чтение происходит вместе с вложенным файлом (который имеет текст, по типу json, xml, txt и других) с помощью парсинга данных

## Для тестирования
1. Скачайте репозиторий гит
2. Измените код в классах ReaderEmail (Addressees) и EmailService (Sender). Вместо написанного текста нужно вставить соответствующие данные (smtpUsername="email@email.com" и т.д.)
3. Запустить программу