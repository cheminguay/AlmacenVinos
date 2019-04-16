# AlmacenVinos

Esta aplicación gestiona un almacén de vinos, donde existen vinos, botellas de cada vino con su añada y caducidad y las existencias de botellas en la bodega.

Aparte de la funcionalidad común de añadir y dar de baja cada uno de las clases, tembién existe un proceso que notifica cada entrada y salida de la bodega y también avisa de la próxima caducidad de las botellas que están en el almacén.

La aplicación consta de;
- una WebApi que gestiona la persistencia de los datos.
- Entity Framework para el acceso a los datos.
- Un proceso Batch para enviar las notificaciones de caducidad.
- Una Web MVC para visualizar los datos y gestionarlos.
- Un proyecto de pruebas unitarias para la capa de servicios.

Así mismo se hace uso de Unity para la inyección de dependencia.

El consumo de la WebApi se hace desde el backend de la aplicación Web MVC, aunque actualmente es más habitual hacerlo desde el frontend con herramientas como angular y type script, aquí tan sólo se pretende hacer una muestra de usus de .NET.

Entity Framework es DataBase First, por lo cual a continuación dejo un script para la creacion de la base de datos.



USE [Bodega]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Vino] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]       VARCHAR (50)   COLLATE Modern_Spanish_CI_AI NOT NULL,
    [Denominacion] VARCHAR (50)   NOT NULL,
    [Capacidad]    DECIMAL (4, 2) NULL,
    [Variedad]     VARCHAR (50)   NULL,
    [Crianza]      VARCHAR (50)   NULL,
    [Color]        VARCHAR (50)   NULL,
    [Baja]         BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

CREATE TABLE [dbo].[Botella] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Descripcion] VARCHAR (50) COLLATE Modern_Spanish_CI_AI NOT NULL,
    [Caducidad]   DATETIME     NULL,
    [Añada]       INT          NULL,
    [Disponible]  BIT          DEFAULT ((1)) NOT NULL,
    [IdVino]      INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Botella_Vino] FOREIGN KEY ([IdVino]) REFERENCES [dbo].[Vino] ([Id])
);

GO

CREATE TABLE [dbo].[Bodega] (
    [Id]                  INT IDENTITY (1, 1) NOT NULL,
    [NotificadoCaducidad] BIT DEFAULT ((0)) NOT NULL,
    [IdBotella]           INT NOT NULL,
    [Unidades]            INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bodega_Botella] FOREIGN KEY ([IdBotella]) REFERENCES [dbo].[Botella] ([Id])
);

GO
