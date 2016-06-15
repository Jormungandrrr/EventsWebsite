--##############################################################################
--############################### ICT4Events ###################################
--##############################################################################
--Versie 2
--ASP.NET MVC5 RAZOR
--UpdateLog
--Added TRIGGER randomgen hash
--Added table Tumbnail
--Added table Drops
--Given FK propper names
--SubProductCatID NOT NULL removed
--Added autoincrement triggers for all tables
--Added stored procedure to deactivate wristbands from expired event
--Default value accesslevel = 1
--ForeignKey toegevoegd aan Persoon tabel die verwijst naar AccountID.
--Toevoeging toegevoegd aan Persoon tabel voor een toevoeging van het huisnr.

DROP TABLE Locatie CASCADE CONSTRAINTS;
DROP TABLE Event CASCADE CONSTRAINTS;
DROP TABLE Plek CASCADE CONSTRAINTS;
DROP TABLE Specificatie CASCADE CONSTRAINTS;
DROP TABLE Reservering CASCADE CONSTRAINTS;
DROP TABLE Polsbandje CASCADE CONSTRAINTS;
DROP TABLE Persoon CASCADE CONSTRAINTS;
DROP TABLE Account CASCADE CONSTRAINTS;
DROP TABLE Bijdrage CASCADE CONSTRAINTS;
DROP TABLE Verhuur CASCADE CONSTRAINTS;
DROP TABLE Exemplaar CASCADE CONSTRAINTS;
DROP TABLE Product CASCADE CONSTRAINTS;
DROP TABLE ProductCat CASCADE CONSTRAINTS;
DROP TABLE Categorie CASCADE CONSTRAINTS;
DROP TABLE Reservering_Polsbandje CASCADE CONSTRAINTS; 
DROP TABLE Thumbnail CASCADE CONSTRAINTS;
DROP TABLE account_bijdrage CASCADE CONSTRAINTS; 
DROP TABLE bericht CASCADE CONSTRAINTS; 
DROP TABLE bestand CASCADE CONSTRAINTS; 
DROP TABLE bijdrage_bericht CASCADE CONSTRAINTS;
DROP TABLE bijdrage_categorie CASCADE CONSTRAINTS; 
DROP TABLE plek_reservering CASCADE CONSTRAINTS; 
DROP TABLE plek_specificatie CASCADE CONSTRAINTS; 

DROP SEQUENCE seq_Locatie;
DROP SEQUENCE seq_Event;
DROP SEQUENCE seq_Plek;
DROP SEQUENCE seq_Specificatie;
DROP SEQUENCE seq_Reservering;
DROP SEQUENCE seq_Polsbandje;
DROP SEQUENCE seq_Persoon;
DROP SEQUENCE seq_Account;
DROP SEQUENCE seq_Bijdrage;
DROP SEQUENCE seq_Verhuur;
DROP SEQUENCE seq_Exemplaar;
DROP SEQUENCE seq_Product;
DROP SEQUENCE seq_ProductCat;
DROP SEQUENCE seq_Categorie;
DROP SEQUENCE seq_Reservering_Polsbandje; 
DROP SEQUENCE seq_Thumbnail;

CREATE SEQUENCE seq_Locatie;
CREATE SEQUENCE seq_Event;
CREATE SEQUENCE seq_Plek;
CREATE SEQUENCE seq_Specificatie;
CREATE SEQUENCE seq_Reservering;
CREATE SEQUENCE seq_Polsbandje;
CREATE SEQUENCE seq_Persoon;
CREATE SEQUENCE seq_Account;
CREATE SEQUENCE seq_Bijdrage;
CREATE SEQUENCE seq_Verhuur;
CREATE SEQUENCE seq_Exemplaar;
CREATE SEQUENCE seq_Product;
CREATE SEQUENCE seq_ProductCat;
CREATE SEQUENCE seq_Categorie;
CREATE SEQUENCE seq_Reservering_Polsbandje; 
CREATE SEQUENCE seq_Thumbnail;

--##############################################################################
--############################### CREATE TABLE #################################
--##############################################################################

CREATE TABLE Locatie (
  LocatieID number(10) NOT NULL, 
  naam      varchar2(255), 
  straat    varchar2(255) NOT NULL, 
  nr        number(10) NOT NULL, 
  postcode  varchar2(255) NOT NULL, 
  plaats    varchar2(255) NOT NULL, 
  PRIMARY KEY (LocatieID));
CREATE TABLE Event (
  EventID      number(10) NOT NULL, 
  LocatieID    number(10) NOT NULL, 
  naam         varchar2(255) NOT NULL, 
  datumstart   date NOT NULL, 
  datumeinde   date NOT NULL, 
  maxbezoekers number(10), 
  PRIMARY KEY (EventID));
CREATE TABLE Plek (
  PlekID     number(10) NOT NULL, 
  LocatieID  number(10) NOT NULL, 
  nummer     number(10), 
  capaciteit number(10), 
  PRIMARY KEY (PlekID));
CREATE TABLE Specificatie (
  SpecificatieID number(10) NOT NULL, 
  naam           varchar2(255), 
  PRIMARY KEY (SpecificatieID));
CREATE TABLE Reservering (
  ReserveringID number(10) NOT NULL, 
  PersoonID     number(10) NOT NULL, 
  datumstart    date, 
  datumeinde    date, 
  betaald       number(1), 
  PRIMARY KEY (ReserveringID));
CREATE TABLE Polsbandje (
  PolsbandjeID number(10) NOT NULL, 
  barcode      number(12) NOT NULL, 
  actief       number(1) NOT NULL, 
  PRIMARY KEY (PolsbandjeID));
CREATE TABLE Persoon (
  PersoonID     number(10) NOT NULL, 
  AccountID     number(10) NOT NULL, 
  voornaam      varchar2(255) NOT NULL,
  tussenvoegsel varchar2(255), 
  achternaam    varchar2(255) NOT NULL, 
  straat        varchar2(255) NOT NULL, 
  huisnr        number(10) NOT NULL,
  toevoeging    varchar2(10),
  woonplaats    varchar2(255) NOT NULL,
  banknr        varchar2(255),
  PRIMARY KEY (PersoonID));
CREATE TABLE Account (
  AccountID      number(10) NOT NULL, 
  gebruikersnaam varchar2(255) NOT NULL UNIQUE, 
  email          varchar2(255) NOT NULL UNIQUE, 
  activatiehash  varchar2(255),
  accesslevel    number(2) DEFAULT 1,
  geactiveerd    number(1) DEFAULT 1, 
  PRIMARY KEY (AccountID));
CREATE TABLE Bijdrage (
  BijdrageID number(10) NOT NULL, 
  AccountID  number(10) NOT NULL, 
  PRIMARY KEY (BijdrageID));
CREATE TABLE Verhuur (
  VerhuurID                number(10) NOT NULL, 
  Reservering_PolsbandjeID number(10) NOT NULL, 
  ExemplaarID              number(10) NOT NULL, 
  datumin                  date NOT NULL, 
  datumuit                 date NOT NULL, 
  prijs                    double precision, 
  betaald                  number(1), 
  PRIMARY KEY (VerhuurID));
CREATE TABLE Exemplaar (
  ExemplaarID number(10) NOT NULL, 
  ProductID   number(10) NOT NULL, 
  volgnummer  number(10), 
  barcode     number(12), 
  PRIMARY KEY (ExemplaarID));
CREATE TABLE Product (
  ProductID    number(10) NOT NULL, 
  ProductCatID number(10) NOT NULL, 
  merk         varchar2(255) NOT NULL, 
  serie        varchar2(255), 
  typenummer   number(10), 
  prijs        double precision NOT NULL, 
  PRIMARY KEY (ProductID));
CREATE TABLE Thumbnail(
  ThumbnailID	number(10) NOT NULL,
  imgsource	varchar2(500) NOT NULL,
  title		varchar2(255) NOT NULL,
  actionname	varchar2(255) NOT NULL,
  controllername varchar2(255) NOT NULL,
  description	varchar2(1000) NOT NULL,
  accesslevel	number(2) NOT NULL,
  PRIMARY KEY (ThumbnailID));
CREATE TABLE ProductCat (
  ProductCatID    number(10) NOT NULL, 
  SubProductCatID number(10), 
  naam            varchar2(255) NOT NULL, 
  PRIMARY KEY (ProductCatID));
CREATE TABLE Categorie (
  CategorieID number(10) NOT NULL,
  SubCategorieID number(10),
  naam        varchar2(255) NOT NULL, 
  PRIMARY KEY (CategorieID));
CREATE TABLE Bestand (
  BijdrageID      number(10) NOT NULL, 
  bestandslocatie varchar2(255) NOT NULL, 
  grootte         number(10), 
  PRIMARY KEY (BijdrageID));
CREATE TABLE Bericht (
  BijdrageID number(10) NOT NULL, 
  titel      varchar2(255), 
  inhoud     varchar2(255) NOT NULL, 
  PRIMARY KEY (BijdrageID));
CREATE TABLE Plek_Reservering (
  PlekID        number(10) NOT NULL, 
  ReserveringID number(10) NOT NULL, 
  PRIMARY KEY (PlekID, 
  ReserveringID));
CREATE TABLE Plek_Specificatie (
  PlekID         number(10) NOT NULL, 
  SpecificatieID number(10) NOT NULL, 
  PRIMARY KEY (PlekID, 
  SpecificatieID));
CREATE TABLE Reservering_Polsbandje (
  ID            number(10) NOT NULL, 
  ReserveringID number(10) NOT NULL, 
  PolsbandjeID  number(10) NOT NULL, 
  AccountID     number(10) NOT NULL, 
  aanwezig      number(1) NOT NULL, 
  PRIMARY KEY (ID));
CREATE TABLE Account_Bijdrage (
  AccountID  number(10) NOT NULL, 
  BijdrageID number(10) NOT NULL, 
  "like"     number(1) NOT NULL, 
  ongewenst  number(1) NOT NULL, 
  PRIMARY KEY (AccountID, 
  BijdrageID));
CREATE TABLE Bijdrage_Bericht (
  BijdrageID number(10) NOT NULL, 
  BerichtID  number(10) NOT NULL, 
  PRIMARY KEY (BijdrageID, 
  BerichtID));
CREATE TABLE Bijdrage_Categorie (
  BijdrageID  number(10) NOT NULL, 
  CategorieID number(10) NOT NULL, 
  PRIMARY KEY (BijdrageID, 
  CategorieID));
--##############################################################################
--############################### CONSTRAINTS ##################################
--##############################################################################
--Constraint name FK(Sub)[Table][PointingTable]  -> FK = ForiegnKey | (Sub) = optioneel subcategorie | [Table] = Naam van table die FK krijgt | [PointingTable] = naam van table waar naar verwezen wordt.
ALTER TABLE Event 
	ADD CONSTRAINT FKEventLocatie
	FOREIGN KEY (LocatieID) REFERENCES Locatie (LocatieID);
ALTER TABLE Plek 
	ADD CONSTRAINT FKPlekLocatie
	FOREIGN KEY (LocatieID) REFERENCES Locatie (LocatieID);
ALTER TABLE Plek_Reservering 
	ADD CONSTRAINT FKPlek_ReserveringPlek
	FOREIGN KEY (PlekID) REFERENCES Plek (PlekID);
ALTER TABLE Plek_Reservering 
	ADD CONSTRAINT FKPlek_ResReservering
	FOREIGN KEY (ReserveringID) REFERENCES Reservering (ReserveringID);
ALTER TABLE Plek_Specificatie 
	ADD CONSTRAINT FKPlek_SpecificatiePlek
	FOREIGN KEY (PlekID) REFERENCES Plek (PlekID);
ALTER TABLE Plek_Specificatie 
	ADD CONSTRAINT FKPlek_SpecSpecificatie
	FOREIGN KEY (SpecificatieID) REFERENCES Specificatie (SpecificatieID);
ALTER TABLE Reservering 
	ADD CONSTRAINT FKReserveringPersoon
	FOREIGN KEY (PersoonID) REFERENCES Persoon (PersoonID);
ALTER TABLE Reservering_Polsbandje 
	ADD CONSTRAINT FKReservering_PolsRes
	FOREIGN KEY (ReserveringID) REFERENCES Reservering (ReserveringID);
ALTER TABLE Reservering_Polsbandje 
	ADD CONSTRAINT FKRes_PolsbandjePols
	FOREIGN KEY (PolsbandjeID) REFERENCES Polsbandje (PolsbandjeID);
ALTER TABLE Reservering_Polsbandje 
	ADD CONSTRAINT FKRes_PolsbandjeAccount
	FOREIGN KEY (AccountID) REFERENCES Account (AccountID);
ALTER TABLE Verhuur 
	ADD CONSTRAINT FKVerhuurRes_Polsbandje
	FOREIGN KEY (Reservering_PolsbandjeID) REFERENCES Reservering_Polsbandje (ID);
ALTER TABLE Verhuur 
	ADD CONSTRAINT FKVerhuurExemplaar
	FOREIGN KEY (ExemplaarID) REFERENCES Exemplaar (ExemplaarID);
ALTER TABLE Exemplaar 
	ADD CONSTRAINT FKExemplaarProduct
	FOREIGN KEY (ProductID) REFERENCES Product (ProductID);
ALTER TABLE Product 
	ADD CONSTRAINT FKProductProductCat
	FOREIGN KEY (ProductCatID) REFERENCES ProductCat (ProductCatID);
ALTER TABLE ProductCat 
	ADD CONSTRAINT FKSubproductcat 
	FOREIGN KEY (SubProductCatID) REFERENCES ProductCat (ProductCatID);
ALTER TABLE Bijdrage 
	ADD CONSTRAINT FKBijdrageAccount
	FOREIGN KEY (AccountID) REFERENCES Account (AccountID);
ALTER TABLE Account_Bijdrage 
	ADD CONSTRAINT FKAccount_BijdrageAccount
	FOREIGN KEY (AccountID) REFERENCES Account (AccountID);
ALTER TABLE Account_Bijdrage 
	ADD CONSTRAINT FKAccount_BijdrageBijdrage
	FOREIGN KEY (BijdrageID) REFERENCES Bijdrage (BijdrageID);
ALTER TABLE Bijdrage_Bericht 
	ADD CONSTRAINT FKBijdrage_BerichtBijdrage
	FOREIGN KEY (BijdrageID) REFERENCES Bijdrage (BijdrageID);
ALTER TABLE Bijdrage_Bericht 
	ADD CONSTRAINT FKBijdrage_BerichtBericht
	FOREIGN KEY (BerichtID) REFERENCES Bericht (BijdrageID);
ALTER TABLE Bericht 
	ADD CONSTRAINT FKBerichtBijdrage
	FOREIGN KEY (BijdrageID) REFERENCES Bijdrage (BijdrageID);
ALTER TABLE Bestand 
	ADD CONSTRAINT FKBestandBijdrage
	FOREIGN KEY (BijdrageID) REFERENCES Bijdrage (BijdrageID);
ALTER TABLE Categorie 
	ADD CONSTRAINT FKSubcategorie 
	FOREIGN KEY (SubCategorieID) REFERENCES Categorie (CategorieID);
ALTER TABLE Bijdrage_Categorie 
	ADD CONSTRAINT FKBijdrage_CategorieBijdrage
	FOREIGN KEY (BijdrageID) REFERENCES Bijdrage (BijdrageID);
ALTER TABLE Bijdrage_Categorie 
	ADD CONSTRAINT FKBijdrage_CategorieCategorie
	FOREIGN KEY (CategorieID) REFERENCES Categorie (CategorieID);
ALTER TABLE Persoon 
	ADD CONSTRAINT FKPersoonAccount
	FOREIGN KEY (AccountID) REFERENCES Account (AccountID);
--##############################################################################
--############################### TRIGGER HASH #################################
--##############################################################################
CREATE OR REPLACE TRIGGER trigger_ActivatieHash
  BEFORE INSERT ON account
  FOR EACH ROW
BEGIN
	--Random value die hash voorstelt
   :NEW.ACTIVATIEHASH := dbms_random.value(100000000000, 999999999999);
END;
/
--##############################################################################
--############################### DEACTIVATE POLSBANDJE ########################
--##############################################################################
CREATE OR REPLACE PROCEDURE DeactivatePolsbandjes
AS
	--Alle reserveringen die na huidige datum zijn
  CURSOR c_EventReservering IS
    SELECT
      r.reserveringID
    FROM reservering r
    WHERE r.DATUMEINDE < SYSDATE;
BEGIN
	--Alle rijen uit de cursor
  FOR rij IN c_EventReservering LOOP
	--Deactiveer polsbandje van reservering
    UPDATE Polsbandje
      SET actief = 0
      WHERE polsbandjeID IN
      (
        SELECT polsbandjeID
        FROM reservering_polsbandje
        WHERE reserveringID = rij.reserveringID
      );
  END LOOP;
END;
/
--##############################################################################
--############################### NOT IMPLIMETED ########################
--##############################################################################
CREATE OR REPLACE PROCEDURE DeactivatePolsbandjes(
	
)
AS

BEGIN

END;
/
--##############################################################################
--############################### AUTO INCREMENT TRIGGERS ######################
--##############################################################################
CREATE OR REPLACE TRIGGER AutoidTriggerLocatie
	BEFORE INSERT ON Locatie FOR EACH ROW
	WHEN(new.locatieID is null)
BEGIN
	SELECT seq_Locatie.NEXTVAL 
	INTO :new.locatieID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerEvent
	BEFORE INSERT ON Event FOR EACH ROW
	WHEN(new.eventID is null)
BEGIN
	SELECT seq_Event.NEXTVAL 
	INTO :new.eventID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerPlek
	BEFORE INSERT ON Plek FOR EACH ROW
	WHEN(new.plekID is null)
BEGIN
	SELECT seq_Plek.NEXTVAL 
	INTO :new.plekID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerSpecificatie
	BEFORE INSERT ON Specificatie FOR EACH ROW
	WHEN(new.specificatieID is null)
BEGIN
	SELECT seq_Specificatie.NEXTVAL 
	INTO :new.specificatieID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerReservering
	BEFORE INSERT ON Reservering FOR EACH ROW
	WHEN(new.reserveringID is null)
BEGIN
	SELECT seq_Reservering.NEXTVAL 
	INTO :new.reserveringID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerPolsbandje
	BEFORE INSERT ON Polsbandje FOR EACH ROW
	WHEN(new.polsbandjeID is null)
BEGIN
	SELECT seq_Polsbandje.NEXTVAL 
	INTO :new.polsbandjeID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerPersoon
	BEFORE INSERT ON Persoon FOR EACH ROW
	WHEN(new.persoonID is null)
BEGIN
	SELECT seq_Persoon.NEXTVAL 
	INTO :new.persoonID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerAccount
	BEFORE INSERT ON Account FOR EACH ROW
	WHEN(new.accountID is null)
BEGIN
	SELECT seq_Account.NEXTVAL 
	INTO :new.accountID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerBijdrage
	BEFORE INSERT ON Bijdrage FOR EACH ROW
	WHEN(new.bijdrageID is null)
BEGIN
	SELECT seq_Bijdrage.NEXTVAL 
	INTO :new.bijdrageID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerVerhuur
	BEFORE INSERT ON Verhuur FOR EACH ROW
	WHEN(new.verhuurID is null)
BEGIN
	SELECT seq_Verhuur.NEXTVAL 
	INTO :new.verhuurID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerExemplaar
	BEFORE INSERT ON Exemplaar FOR EACH ROW
	WHEN(new.exemplaarID is null)
BEGIN
	SELECT seq_Exemplaar.NEXTVAL 
	INTO :new.exemplaarID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerProduct
	BEFORE INSERT ON Product FOR EACH ROW
	WHEN(new.productID is null)
BEGIN
	SELECT seq_Product.NEXTVAL 
	INTO :new.productID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerProductCat
	BEFORE INSERT ON ProductCat FOR EACH ROW
	WHEN(new.ProductCatID is null)
BEGIN
	SELECT seq_ProductCat.NEXTVAL 
	INTO :new.ProductCatID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerCategorie
	BEFORE INSERT ON Categorie FOR EACH ROW
	WHEN(new.CategorieID is null)
BEGIN
	SELECT seq_Categorie.NEXTVAL 
	INTO :new.CategorieID 
	FROM dual;
END;
/

CREATE OR REPLACE TRIGGER AutoidTriggerRes_Polsbandje
	BEFORE INSERT ON Reservering_Polsbandje FOR EACH ROW
	WHEN(new.ID is null)
BEGIN
	SELECT seq_Reservering_Polsbandje.NEXTVAL 
	INTO :new.ID 
	FROM dual;
END;
/
CREATE OR REPLACE TRIGGER AutoidTriggerThumbnail
	BEFORE INSERT ON Thumbnail FOR EACH ROW
	WHEN(new.ThumbnailID is null)
BEGIN
	SELECT seq_Thumbnail.NEXTVAL 
	INTO :new.ThumbnailID 
	FROM dual;
END;
/