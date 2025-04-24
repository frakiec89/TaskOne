
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
