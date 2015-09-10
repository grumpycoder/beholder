CREATE PARTITION SCHEME [BeholderFileStreamDBScheme]
    AS PARTITION [BeholderFileStreamFunction]
    TO ([BeholderData1], [BeholderData2], [BeHolderData3]);

