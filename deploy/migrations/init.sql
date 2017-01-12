CREATE TABLE "__EFMigrationsHistory" (
    "MigrationId" varchar(150) NOT NULL,
    "ProductVersion" varchar(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "KeyCollections" (
    "Id" serial NOT NULL,
    "AppId" uuid NOT NULL,
    "InstanceId" uuid NOT NULL,
    "Timestamp" timestamp NOT NULL,
    "Value" text,
    CONSTRAINT "PK_KeyCollections" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20170112100932_init', '1.0.2');

