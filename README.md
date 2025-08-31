�������� �������: Minimal API + EF Core + PostgreSQL + �������������� �����
����

����������� Minimal API (.NET 9) � ����� ����������� ��� �������� Product, ��������� EF Core � PostgreSQL.
���������� ������ �������������� ��������������� �������.

����������
.NET 9
ASP.NET Core Minimal API
EF Core 9
PostgreSQL 16+
�����: xUnit, Microsoft.AspNetCore.Mvc.Testing, Testcontainers for .NET

�������� � ������ ��
Product
Id uuid (PK, ������������ �� �������)
Name varchar(100) (required, ����������)
Price numeric(18,2) (> 0)
CreatedAt timestamptz (UTC, ������������ �� �������)

���������� � �����
��� ����������� � ���� � ����� Fluent API + ��������.
������ �� ���� Name.

���������
POST /api/products
���� �������: { "name": string, "price": decimal }

���������:
name �� ������, ����� 2�100
price > 0

���������: ������� �������, Id � CreatedAt ����������� �� �������

������:
200 Created � Location: /products/{id} � ����� ���������� ��������
400 Bad Request � ������ ��������� (������� ����/�������)

GET /products/{id:guid}
���������� ������� �� Id

������:
200 OK � ������
400 Not Found � �� ������

���������������� ����������
��������� DTO �� ����/����� (�� ������� �������� ������ ��������)

������������ ������ ����������� � appsettings.json
����������� ��� (EF Core async)