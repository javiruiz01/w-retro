/** Dummy data */

USE wretro;

DECLARE @SESSION_ID UNIQUEIDENTIFIER = N'8986E7EF-6FF4-49EC-BAEA-EFC21576D649';
DECLARE @CARD_ID_1 UNIQUEIDENTIFIER = N'2591EE0D-4178-47CA-BFC2-FA5304F925B8';
DECLARE @CARD_ID_2 UNIQUEIDENTIFIER = N'10435192-6C10-4CD0-9C36-EC539B364606';
DECLARE @CARD_ID_3 UNIQUEIDENTIFIER = N'7C28AD1F-601E-4E31-AEEF-5FCAE1B204B5';

-- Session
INSERT INTO [wretro].[Session]
  (Id)
VALUES
  (@SESSION_ID);

-- Cards
INSERT INTO [wretro].[Card]
  (Id, Title, Position, SessionId)
VALUES
  (@CARD_ID_1, 'Good', 0, @SESSION_ID);

INSERT INTO [wretro].[Card]
  (Id, Title, Position, SessionId)
VALUES
  (@CARD_ID_2, 'Meh', 1, @SESSION_ID);

INSERT INTO [wretro].[Card]
  (Id, Title, Position, SessionId)
VALUES
  (@CARD_ID_3, 'Bad', 2, @SESSION_ID);

-- Comments
INSERT INTO [wretro].[Comment]
  (Id, Text, Likes, CardId)
VALUES
  ('797CDEF9-8FBD-4F1C-B8AA-D679B235B85D', 'So much work!', 0, @CARD_ID_1);

INSERT INTO [wretro].[Comment]
  (Id, Text, Likes, CardId)
VALUES
  ('F408F188-5C35-473A-8275-B2D2218BF0DA', 'Good things are good', 0, @CARD_ID_1);

INSERT INTO [wretro].[Comment]
  (Id, Text, Likes, CardId)
VALUES
  ('65CE9F39-6C85-4238-A0E2-0D7C67602765', 'Innovation week canceled', 0, @CARD_ID_2);

INSERT INTO [wretro].[Comment]
  (Id, Text, Likes, CardId)
VALUES
  ('DB37F5F9-50BF-407C-975F-DAE47F55C246', 'Innovation week canceled', 0, @CARD_ID_2);

INSERT INTO [wretro].[Comment]
  (Id, Text, Likes, CardId)
VALUES
  ('D5BB2620-3257-435D-A130-25E34AE6803D', 'Something about infra', 0, @CARD_ID_3);

INSERT INTO [wretro].[Comment]
  (Id, Text, Likes, CardId)
VALUES
  ('1730E882-B979-47F6-96C6-B8272D4B7716', 'The planning', 0, @CARD_ID_3);
