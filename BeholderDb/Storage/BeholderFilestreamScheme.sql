CREATE PARTITION SCHEME [BeholderFilestreamScheme]
    AS PARTITION [BeholderFileStreamFunction]
    TO ([StreamGroup1], [StreamGroup2], [StreamGroup3]);

