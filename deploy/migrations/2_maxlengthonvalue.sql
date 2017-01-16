ALTER TABLE "KeyCollections" ALTER COLUMN "Value" TYPE varchar(2000);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20170116112029_maxlengthonvalue', '1.0.2');