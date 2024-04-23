# ������� � ���������
helm install hw7 k8s
newman run HW7.postman_collection
helm uninstall hw7

���������� ���������� ��������� � ������ newman_1.png � newman_2.png


# �������� �������������� ������� � ����� �������������� ��������
1.  ������� ������������. ������ ��������� ������� � ��������.
     *BillingService::CreateAccount [REST API / POST]*
2.  �������� ������ �� ���� ������������ ����� ������ ��������.
     *BillingService::IncreaseBalance [REST API / PUT]*
3.  ������� �����, �� ������� ������� �����.
     *OrderService::CreateOrder [REST API / POST] -> BillingService::DecreaseBalance [REST API / PUT] -> OrderService::PublishMessage [RabbitMQ] -> NotificationService::ConsumeMessage [RabbitMQ]*
4.  ���������� ������ �� ����� ������������ � ���������, ��� �� �����.
      *BillingService -> GetAccountById [REST API / GET]*
5.  ���������� � ������� ����������� ������������ ��������� � ���������, ��� ��������� �����������
     *NotificationService::GetLastByAccountId [REST API / GET]*
6.  ������� �����, �� ������� �� ������� �����.
      *��. �. 3*
7.  ���������� ������ �� ����� ������������ � ���������, ��� �� ���������� �� ����������.
     *��. �. 4*
8.  ���������� � ������� ����������� ������������ ��������� � ���������, ��� ��������� �����������.
     *��. �. 5*