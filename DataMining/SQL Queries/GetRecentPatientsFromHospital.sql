--Uncomment for design time testing
--DECLARE
--	@hospitalName AS STRING = 'KCI Port Huron',
--	@daysToSearch AS INT = 10

SELECT pt.PatientId, hosp.HospitalName
FROM ScheduledActivity as sa
    INNER JOIN  Patient as pt on pt.PatientSer = sa.PatientSer
    INNER JOIN  ActivityInstance as ai on sa.ActivityInstanceSer = ai.ActivityInstanceSer
    INNER JOIN  Activity as act on ai.ActivitySer = act.ActivitySer
    INNER JOIN  ActivityCategory as actCat on actCat.ActivityCategorySer = act.ActivityCategorySer
    INNER JOIN  Department as dep on dep.DepartmentSer = ai.DepartmentSer
    INNER JOIN  Hospital as hosp on hosp.HospitalSer = dep.HospitalSer
WHERE  hosp.HospitalName = @hospitalName
    AND (actCat.ActivityCategoryCode = 'Treatment')
    AND (sa.ObjectStatus <> 'Deleted')
    AND (sa.ScheduledStartTime BETWEEN DATEADD(DAY, -@daysToSearch, GETDATE()) AND DATEADD(day, + 1, GETDATE()))
GROUP BY pt.PatientId, hosp.HospitalName
ORDER BY hosp.HospitalName