CREATE ROLE [Beholder_user]
    AUTHORIZATION [dbo];


GO
ALTER ROLE [Beholder_user] ADD MEMBER [SPLC\iis-beholder$];

