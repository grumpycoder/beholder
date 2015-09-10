CREATE SCHEMA [Security]
    AUTHORIZATION [dbo];


GO
GRANT DELETE
    ON SCHEMA::[Security] TO [Beholder_user];


GO
GRANT EXECUTE
    ON SCHEMA::[Security] TO [Beholder_user];


GO
GRANT INSERT
    ON SCHEMA::[Security] TO [Beholder_user];


GO
GRANT SELECT
    ON SCHEMA::[Security] TO [Beholder_user];


GO
GRANT UPDATE
    ON SCHEMA::[Security] TO [Beholder_user];

