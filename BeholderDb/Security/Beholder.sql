CREATE SCHEMA [Beholder]
    AUTHORIZATION [dbo];


GO
GRANT DELETE
    ON SCHEMA::[Beholder] TO [Beholder_user];


GO
GRANT EXECUTE
    ON SCHEMA::[Beholder] TO [Beholder_user];


GO
GRANT INSERT
    ON SCHEMA::[Beholder] TO [Beholder_user];


GO
GRANT SELECT
    ON SCHEMA::[Beholder] TO [Beholder_user];


GO
GRANT UPDATE
    ON SCHEMA::[Beholder] TO [Beholder_user];

