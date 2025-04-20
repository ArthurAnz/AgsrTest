CREATE OR ALTER PROCEDURE SearchPatientsByBirthDateFhir
    @Modifier1 NVARCHAR(2) = NULL,
    @DateTime1 DATETIME2 = NULL,
    @Modifier2 NVARCHAR(2) = NULL,
    @DateTime2 DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Patients
    WHERE
        (
            @Modifier1 IS NULL OR
            (
                (@Modifier1 = 'eq' AND BirthDate = @DateTime1) OR
                (@Modifier1 = 'ne' AND BirthDate != @DateTime1) OR
                (@Modifier1 = 'gt' AND BirthDate > @DateTime1) OR
                (@Modifier1 = 'lt' AND BirthDate < @DateTime1) OR
                (@Modifier1 = 'ge' AND BirthDate >= @DateTime1) OR
                (@Modifier1 = 'le' AND BirthDate <= @DateTime1) OR
                (@Modifier1 = 'sa' AND BirthDate = @DateTime1) OR
                (@Modifier1 = 'eb' AND BirthDate <= @DateTime1) OR
                (@Modifier1 = 'ap' AND BirthDate >= @DateTime1)
            )
        )
        AND
        (
            @Modifier2 IS NULL OR
            (
                (@Modifier2 = 'eq' AND BirthDate = @DateTime2) OR
                (@Modifier2 = 'ne' AND BirthDate != @DateTime2) OR
                (@Modifier2 = 'gt' AND BirthDate > @DateTime2) OR
                (@Modifier2 = 'lt' AND BirthDate < @DateTime2) OR
                (@Modifier2 = 'ge' AND BirthDate >= @DateTime2) OR
                (@Modifier2 = 'le' AND BirthDate <= @DateTime2) OR
                (@Modifier2 = 'sa' AND BirthDate = @DateTime2) OR
                (@Modifier2 = 'eb' AND BirthDate <= @DateTime2) OR
                (@Modifier2 = 'ap' AND BirthDate >= @DateTime2)
            )
        );
END
