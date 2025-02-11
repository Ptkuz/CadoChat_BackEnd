🔽 Компоненты системы:

1️⃣ API Gateway
📌 Отвечает за маршрутизацию запросов к микросервисам.
✅ Реализуется через Ocelot.
✅ Включает JWT-аутентификацию.

2️⃣ Auth Service (Авторизация и аутентификация)
📌 Обрабатывает регистрацию, вход, OAuth2, JWT.
✅ Хранение пользователей в PostgreSQL.
✅ Использует встроенный ASP.NET Identity.

3️⃣ Chat Service (Текстовые чаты)
📌 Основной микросервис для общения.
✅ SignalR – реализация WebSocket для мгновенных сообщений.
✅ MongoDB – хранение сообщений (NoSQL подходит для чатов).
✅ RabbitMQ – для обработки входящих сообщений.

4️⃣ Call Service (Видеозвонки)
📌 Обрабатывает видеозвонки через WebRTC.
✅ Сигнализация через SignalR (WebSocket).
✅ STUN/TURN-серверы (например, Coturn) для работы за NAT.

5️⃣ Notification Service (Уведомления)
📌 Обрабатывает пуш-уведомления (Firebase, Email).
✅ Подписка на события через RabbitMQ.
✅ Поддержка Web Push API для браузеров.

6️⃣ Presence Service (Онлайн-статус)
📌 Управляет статусами пользователей (онлайн/офлайн).
✅ Использует Redis Pub/Sub для быстрой обработки.

7️⃣ Media Service (Хранение файлов, изображений, видео)
📌 Отвечает за загрузку и хранение медиафайлов.
✅ MinIO (S3-совместимое хранилище) или Azure Blob Storage.

8️⃣ Logging & Monitoring (Логирование и мониторинг)
📌 Сбор логов и метрик.
✅ Serilog + Elasticsearch + Kibana.
✅ Prometheus + Grafana для мониторинга.
