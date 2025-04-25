
task 3 
```sql
DECLARE @count  INT, @Year INT, @StepDate DATE
SET @count = 0;
SET @Year = YEAR(GETDATE())
SET @StepDate = CAST(CONCAT(@Year, '-01-01') AS DATE)

WHILE @count < 366
BEGIN
    IF YEAR(@StepDate) <> @Year
        BREAK

    PRINT FORMAT(@StepDate, 'dd MMMM yyyy', 'ru-RU')

    SET @StepDate = DATEADD(DAY, 1, @StepDate)
    SET @count = @count + 1
END
```

task  4
```sql
CREATE  Database  Credit_Test_DB
COLLATE Cyrillic_General_100_CI_AS_SC_UTF8; -- в  локал дб почему то обязательный, 
GO

USE Credit_Test_DB
GO

CREATE  TABLE Сredit_Test
(
	Id int primary key identity (1,1) , 
	NameUser nvarchar (100) UNIQUE not null , -- todo нормализовать
	Summa Money  CHECK(Summa>0) not null ,
	CountMonths int CHECK (CountMonths > 0) NOT NULL ,
	DateStart date NOT NULL default Getdate(), 
	RateMonth decimal (5,5) CHECK(RateMonth>0)NOT NULL 
)

INSERT INTO Сredit_Test (NameUser ,Summa  , CountMonths , RateMonth)
values 
('Ахтямов' , 2000000 , 60 , 0.0125);

DECLARE @period INT ,  -- скрок  кредита  в  месяцах
@Summa Money  ,   -- сумма кредита
@RateMonth decimal(5,5) ,  -- процентная ставка 
@K_an decimal(5,5)  ,   --   коефециент аннуитета
@P_M decimal(18,3) ,      -- платеж  в  месяц
@TottalSumma decimal(18,3)  -- догл  с процентами


SET @period  =  (Select  TOP (1) CountMonths  from Сredit_Test Where NameUser = 'Ахтямов' )
SET @Summa   =  (Select  TOP (1) Summa  from Сredit_Test Where NameUser = 'Ахтямов' )
SET @RateMonth  =  (Select  TOP (1) RateMonth  from Сredit_Test Where NameUser = 'Ахтямов' )


SET @K_an = (@RateMonth * ( Power( 1+ @RateMonth , @period ))) / (POWER( 1+ @RateMonth , @period  )-1) -- по формуле
SET @P_M = @Summa  *  @K_an   -- по формуле
SET @TottalSumma = @P_M * @period


WHILE @period > 0
    BEGIN
        SET @period = @period-1
		SET @TottalSumma = @TottalSumma - @P_M 

		if @TottalSumma>=@Summa
			Print  CONCAT ('Месяц: ' , @period , ' Платеж: ' , @P_M  , ' Основной долг :' ,@Summa  , ' Остаток по процентам:', @TottalSumma -@Summa ) 
		if @TottalSumma<@Summa
			Print  CONCAT ('Месяц: ' , @period , ' Платеж: ' , @P_M  , ' долг :' ,@TottalSumma   ) 

    END;
 
 --USE master 
--DROP Database  Credit_Test_DB

-- SELECT * FROM Сredit_Test
-- DELETE FROM Сredit_Test

```
