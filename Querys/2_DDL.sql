-- Generado por Oracle SQL Developer Data Modeler 20.2.0.167.1538
--   en:        2022-11-29 15:45:25 CST
--   sitio:      SQL Server 2012
--   tipo:      SQL Server 2012



CREATE TABLE APUESTA 
    (
     ID_JORNADA VARCHAR (10) NOT NULL , 
     ID_USUARIO INTEGER NOT NULL , 
     ID_LIGA INTEGER NOT NULL , 
     ID_EQUIPOLOCAL VARCHAR (10) NOT NULL , 
     ID_EQUIPOVISITANTE VARCHAR (10) NOT NULL , 
     FECHA DATETIME DEFAULT GETDATE() NOT NULL , 
     APUESTA_LOCAL INTEGER NOT NULL , 
     APUESTA_VISITANTE INTEGER NOT NULL , 
     PUNTOS_OBTENIDOS INTEGER , 
     ID_ROL INTEGER NOT NULL 
    )
GO

ALTER TABLE APUESTA ADD CONSTRAINT APUESTA_PK PRIMARY KEY CLUSTERED (ID_USUARIO, ID_JORNADA, ID_LIGA, ID_EQUIPOLOCAL, ID_EQUIPOVISITANTE)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE EQUIPOS 
    (
     ID_EQUIPO VARCHAR (10) NOT NULL , 
     NOMBRE_EQUIPO VARCHAR (20) , 
     ID_GRUPO VARCHAR (10) NOT NULL 
    )
GO

ALTER TABLE EQUIPOS ADD CONSTRAINT EQUIPOS_PK PRIMARY KEY CLUSTERED (ID_EQUIPO)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE ESTADIOS 
    (
     ID_ESTADIO VARCHAR (10) NOT NULL , 
     NOMBRE_ESTADIO VARCHAR (30) NOT NULL 
    )
GO

ALTER TABLE ESTADIOS ADD CONSTRAINT ESTADIOS_PK PRIMARY KEY CLUSTERED (ID_ESTADIO)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE GRUPOS 
    (
     ID_GRUPO VARCHAR (10) NOT NULL , 
     NOMBRE_GRUPO VARCHAR (20) NOT NULL , 
     ID_TORNEO INTEGER NOT NULL 
    )
GO

ALTER TABLE GRUPOS ADD CONSTRAINT GRUPOS_PK PRIMARY KEY CLUSTERED (ID_GRUPO)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE INVITACIONES 
    (
     ID_USUARIO INTEGER NOT NULL , 
     ID_LIGA INTEGER NOT NULL , 
     STATUS INTEGER NOT NULL 
    )
GO

ALTER TABLE INVITACIONES ADD CONSTRAINT INVITACIONES_PK PRIMARY KEY CLUSTERED (ID_USUARIO, ID_LIGA)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE JORNADA 
    (
     ID_JORNADA VARCHAR (10) NOT NULL , 
     NOMBRE_JORNADA VARCHAR (20) NOT NULL 
    )
GO

ALTER TABLE JORNADA ADD CONSTRAINT JORNADA_PK PRIMARY KEY CLUSTERED (ID_JORNADA)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE LIGA 
    (
     ID_LIGA INTEGER IDENTITY(1,1) NOT NULL , 
     PRECIO NUMERIC (10,2) NOT NULL , 
     NOMBRE_LIGA VARCHAR (20) NOT NULL , 
     ID_TIPOLIGA INTEGER NOT NULL 
    )
GO

ALTER TABLE LIGA ADD CONSTRAINT LIGA_PK PRIMARY KEY CLUSTERED (ID_LIGA)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE PAGO 
    (
     ID_PAGO INTEGER IDENTITY(1,1) NOT NULL , 
     ID_USUARIO INTEGER NOT NULL , 
     ID_LIGA INTEGER NOT NULL , 
     ID_ROL INTEGER NOT NULL , 
     NUMERO_TARJETA INTEGER NOT NULL , 
     FECHA_EXPIRACION DATE NOT NULL , 
     MONTO NUMERIC (10,2) NOT NULL 
    )
GO

ALTER TABLE PAGO ADD CONSTRAINT PAGO_PK PRIMARY KEY CLUSTERED (ID_PAGO)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE PARTIDOS 
    (
     ID_JORNADA VARCHAR (10) NOT NULL , 
     ID_EQUIPOLOCAL VARCHAR (10) NOT NULL , 
     ID_EQUIPOVISITANTE VARCHAR (10) NOT NULL , 
     ID_ESTADIO VARCHAR (10) NOT NULL , 
     FECHA DATETIME NOT NULL , 
     GOLES_LOCAL INTEGER , 
     GOLES_VISITANTE INTEGER 
    )
GO

ALTER TABLE PARTIDOS ADD CONSTRAINT PARTIDOS_PK PRIMARY KEY CLUSTERED (ID_JORNADA, ID_EQUIPOLOCAL, ID_EQUIPOVISITANTE)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE ROL_USUARIO 
    (
     ID_ROL INTEGER NOT NULL , 
     NOMBRE_ROL VARCHAR (30) NOT NULL 
    )
GO

ALTER TABLE ROL_USUARIO ADD CONSTRAINT ROL_USUARIO_PK PRIMARY KEY CLUSTERED (ID_ROL)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE TIPO_LIGA 
    (
     ID_TIPOLIGA INTEGER NOT NULL , 
     NOMBRE_TIPOLIGA VARCHAR (20) NOT NULL 
    )
GO

ALTER TABLE TIPO_LIGA ADD CONSTRAINT TIPO_LIGA_PK PRIMARY KEY CLUSTERED (ID_TIPOLIGA)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE TORNEO 
    (
     ID_TORNEO INTEGER NOT NULL , 
     NOMBRE_TORNEO VARCHAR (50) NOT NULL 
    )
GO

ALTER TABLE TORNEO ADD CONSTRAINT TORNEO_PK PRIMARY KEY CLUSTERED (ID_TORNEO)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE USUARIO 
    (
     ID_USUARIO INTEGER IDENTITY(1,1) NOT NULL , 
     NOMBRES VARCHAR (50) NOT NULL , 
     APELLIDOS VARCHAR (50) NOT NULL , 
     TELEFONO INTEGER NOT NULL , 
     CORREO VARCHAR (30) NOT NULL , 
     CONTRASEŅA VARCHAR (30) NOT NULL 
    )
GO

ALTER TABLE USUARIO ADD CONSTRAINT USUARIO_PK PRIMARY KEY CLUSTERED (ID_USUARIO)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE USUARIOLIGA 
    (
     ID_USUARIO INTEGER NOT NULL , 
     ID_LIGA INTEGER NOT NULL , 
     ID_ROL INTEGER NOT NULL 
    )
GO

ALTER TABLE USUARIOLIGA ADD CONSTRAINT USUARIOLIGA_PK PRIMARY KEY CLUSTERED (ID_USUARIO, ID_LIGA, ID_ROL)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

ALTER TABLE APUESTA 
    ADD CONSTRAINT APUESTA_PARTIDOS_FK FOREIGN KEY 
    ( 
     ID_JORNADA, 
     ID_EQUIPOLOCAL, 
     ID_EQUIPOVISITANTE
    ) 
    REFERENCES PARTIDOS 
    ( 
     ID_JORNADA , 
     ID_EQUIPOLOCAL , 
     ID_EQUIPOVISITANTE 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE APUESTA 
    ADD CONSTRAINT APUESTA_USUARIOLIGA_FK FOREIGN KEY 
    ( 
     ID_USUARIO, 
     ID_LIGA, 
     ID_ROL
    ) 
    REFERENCES USUARIOLIGA 
    ( 
     ID_USUARIO , 
     ID_LIGA , 
     ID_ROL 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE EQUIPOS 
    ADD CONSTRAINT EQUIPOS_GRUPOS_FK FOREIGN KEY 
    ( 
     ID_GRUPO
    ) 
    REFERENCES GRUPOS 
    ( 
     ID_GRUPO 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE GRUPOS 
    ADD CONSTRAINT GRUPOS_TORNEO_FK FOREIGN KEY 
    ( 
     ID_TORNEO
    ) 
    REFERENCES TORNEO 
    ( 
     ID_TORNEO 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE INVITACIONES 
    ADD CONSTRAINT INVITACIONES_LIGA_FK FOREIGN KEY 
    ( 
     ID_LIGA
    ) 
    REFERENCES LIGA 
    ( 
     ID_LIGA 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE INVITACIONES 
    ADD CONSTRAINT INVITACIONES_USUARIO_FK FOREIGN KEY 
    ( 
     ID_USUARIO
    ) 
    REFERENCES USUARIO 
    ( 
     ID_USUARIO 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE LIGA 
    ADD CONSTRAINT LIGA_TIPO_LIGA_FK FOREIGN KEY 
    ( 
     ID_TIPOLIGA
    ) 
    REFERENCES TIPO_LIGA 
    ( 
     ID_TIPOLIGA 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE PAGO 
    ADD CONSTRAINT PAGO_USUARIOLIGA_FK FOREIGN KEY 
    ( 
     ID_USUARIO, 
     ID_LIGA, 
     ID_ROL
    ) 
    REFERENCES USUARIOLIGA 
    ( 
     ID_USUARIO , 
     ID_LIGA , 
     ID_ROL 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE PARTIDOS 
    ADD CONSTRAINT PARTIDOS_EQUIPOS_FK FOREIGN KEY 
    ( 
     ID_EQUIPOLOCAL
    ) 
    REFERENCES EQUIPOS 
    ( 
     ID_EQUIPO 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE PARTIDOS 
    ADD CONSTRAINT PARTIDOS_EQUIPOS_FKv2 FOREIGN KEY 
    ( 
     ID_EQUIPOVISITANTE
    ) 
    REFERENCES EQUIPOS 
    ( 
     ID_EQUIPO 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE PARTIDOS 
    ADD CONSTRAINT PARTIDOS_ESTADIOS_FK FOREIGN KEY 
    ( 
     ID_ESTADIO
    ) 
    REFERENCES ESTADIOS 
    ( 
     ID_ESTADIO 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE PARTIDOS 
    ADD CONSTRAINT PARTIDOS_JORNADA_FK FOREIGN KEY 
    ( 
     ID_JORNADA
    ) 
    REFERENCES JORNADA 
    ( 
     ID_JORNADA 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE USUARIOLIGA 
    ADD CONSTRAINT USUARIOLIGA_LIGA_FK FOREIGN KEY 
    ( 
     ID_LIGA
    ) 
    REFERENCES LIGA 
    ( 
     ID_LIGA 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE USUARIOLIGA 
    ADD CONSTRAINT USUARIOLIGA_ROL_USUARIO_FK FOREIGN KEY 
    ( 
     ID_ROL
    ) 
    REFERENCES ROL_USUARIO 
    ( 
     ID_ROL 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE USUARIOLIGA 
    ADD CONSTRAINT USUARIOLIGA_USUARIO_FK FOREIGN KEY 
    ( 
     ID_USUARIO
    ) 
    REFERENCES USUARIO 
    ( 
     ID_USUARIO 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO



-- Informe de Resumen de Oracle SQL Developer Data Modeler: 
-- 
-- CREATE TABLE                            14
-- CREATE INDEX                             0
-- ALTER TABLE                             29
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE DATABASE                          0
-- CREATE DEFAULT                           0
-- CREATE INDEX ON VIEW                     0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE ROLE                              0
-- CREATE RULE                              0
-- CREATE SCHEMA                            0
-- CREATE SEQUENCE                          0
-- CREATE PARTITION FUNCTION                0
-- CREATE PARTITION SCHEME                  0
-- 
-- DROP DATABASE                            0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0
