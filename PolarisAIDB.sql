-- ****************** SqlDBM: Microsoft SQL Server ******************
-- ******************************************************************

-- ************************************** [Intent]

CREATE TABLE [Intent]
(
 [intent-name] varchar(30) NOT NULL ,


 CONSTRAINT [PK_Intent] PRIMARY KEY CLUSTERED ([intent-name] ASC)
);
GO

-- ************************************** [Request]

create table Request
(
    [request-id]    bigint identity
        constraint PK_Request
            primary key,
    query           varchar(255)               not null,
    [response-code] int                        not null,
    response        varchar(255),
    [request-time]  datetime default getdate() not null
)
go

-- ************************************** [Entity]

CREATE TABLE [Entity]
(
 [request-id]     bigint NOT NULL ,
 [entity-content] varchar(50) NULL ,
 [start-index]    int NULL ,
 [end-index]      int NULL ,
 [date]           char(10) NULL ,
 [time]           char(8) NULL ,
 [type]           varchar(50) NULL ,


 CONSTRAINT [PK_Entity] PRIMARY KEY CLUSTERED ([request-id] ASC),
 CONSTRAINT [FK_31] FOREIGN KEY ([request-id])  REFERENCES [Request]([request-id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_31] ON [Entity] 
 (
  [request-id] ASC
 )

GO

-- ************************************** [RequestIntent]

CREATE TABLE [RequestIntent]
(
 [request-id]     bigint NOT NULL ,
 [intent-name]    varchar(30) NOT NULL ,
 [is-top-scoring] binary(1) NOT NULL ,
 [intent-score]   float NOT NULL ,


 CONSTRAINT [PK_RequestIntent] PRIMARY KEY CLUSTERED ([request-id] ASC, [intent-name] ASC),
 CONSTRAINT [FK_17] FOREIGN KEY ([request-id])  REFERENCES [Request]([request-id]),
 CONSTRAINT [FK_21] FOREIGN KEY ([intent-name])  REFERENCES [Intent]([intent-name])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_17] ON [RequestIntent] 
 (
  [request-id] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_21] ON [RequestIntent] 
 (
  [intent-name] ASC
 )

GO

