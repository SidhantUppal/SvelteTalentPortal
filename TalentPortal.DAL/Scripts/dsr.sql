WITH FirstDsr AS (
                SELECT dl.Id AS DsrLogId, 
                dl.SubmitDate AS DateTime,
                p.Name AS ProjectName,
                d.Id AS DsrId,
                d.Description,
                d.FromTime,
                d.ToTime,
                ROW_NUMBER() OVER (PARTITION BY dl.Id ORDER BY d.Id) AS RowNumber 
                FROM dsrlog dl 
                INNER JOIN   projects p ON p.Id = (SELECT TOP 1 ProjectId FROM dsrs WHERE CHARINDEX(',' + CAST(Id AS VARCHAR) + ',', ',' + dl.DsrIds + ',') > 0) 
                INNER JOIN  dsrs d ON CHARINDEX(',' + CAST(d.Id AS VARCHAR) + ',', ',' + dl.DsrIds + ',') > 0 
                INNER JOIN Users u on u.Id = dl.UserId 
                WHERE @userId IN (SELECT CAST(value AS INT) FROM STRING_SPLIT(dl.SenderIds, ',')) ) 
                SELECT DsrLogId,  DateTime,  ProjectName,  DsrId, FromTime, ToTime,Description FROM 
                FirstDsr WHERE  RowNumber = 1