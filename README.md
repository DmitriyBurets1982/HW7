# Команды и результат
helm install hw7 k8s </br>
newman run HW7.postman_collection </br>
helm uninstall hw7

Результаты выполнения приложены в файлах newman_1.png и newman_2.png


# Описание архитектурного решения и схема взаимодействия сервисов
1.  Создать пользователя. Должен создаться аккаунт в биллинге. </br>
     *BillingService::CreateAccount [REST API / POST]*
2.  Положить деньги на счет пользователя через сервис биллинга. </br>
     *BillingService::IncreaseBalance [REST API / PUT]*
3.  Сделать заказ, на который хватает денег. </br>
     *OrderService::CreateOrder [REST API / POST] -> BillingService::DecreaseBalance [REST API / PUT] -> OrderService::PublishMessage [RabbitMQ] -> NotificationService::ConsumeMessage [RabbitMQ]*
4.  Посмотреть деньги на счету пользователя и убедиться, что их сняли. </br>
      *BillingService -> GetAccountById [REST API / GET]*
5.  Посмотреть в сервисе нотификаций отправленные сообщения и убедиться, что сообщение отправилось </br>
     *NotificationService::GetLastByAccountId [REST API / GET]*
6.  Сделать заказ, на который не хватает денег. </br>
      *См. п. 3*
7.  Посмотреть деньги на счету пользователя и убедиться, что их количество не поменялось. </br>
     *См. п. 4*
8.  Посмотреть в сервисе нотификаций отправленные сообщения и убедиться, что сообщение отправилось. </br>
     *См. п. 5*
