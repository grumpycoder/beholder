CREATE SCHEMA [Info]
    AUTHORIZATION [dbo];


GO
GRANT EXECUTE
    ON SCHEMA::[Info] TO [Beholder_user];


GO
GRANT SELECT
    ON SCHEMA::[Info] TO [Beholder_user];


GO
EXECUTE sp_addextendedproperty @name = N'Info', @value = N'Information from outside sources', @level0type = N'SCHEMA', @level0name = N'Info';

