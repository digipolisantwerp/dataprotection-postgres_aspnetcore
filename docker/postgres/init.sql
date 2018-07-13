CREATE TABLE "KeyCollections" (
    "Id" serial NOT NULL,
    "AppId" uuid NOT NULL,
    "InstanceId" uuid NOT NULL,
    "Timestamp" timestamp NOT NULL,
    "Value" text,
    CONSTRAINT "PK_KeyCollections" PRIMARY KEY ("Id")
);
ALTER TABLE "KeyCollections" ALTER COLUMN "Value" TYPE varchar(2000);