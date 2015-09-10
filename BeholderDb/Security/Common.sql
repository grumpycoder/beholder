CREATE SCHEMA [Common]
    AUTHORIZATION [dbo];


GO
GRANT DELETE
    ON SCHEMA::[Common] TO [Beholder_user];


GO
GRANT EXECUTE
    ON SCHEMA::[Common] TO [Beholder_user];


GO
GRANT INSERT
    ON SCHEMA::[Common] TO [Beholder_user];


GO
GRANT SELECT
    ON SCHEMA::[Common] TO [Beholder_user];


GO
GRANT UPDATE
    ON SCHEMA::[Common] TO [Beholder_user];

