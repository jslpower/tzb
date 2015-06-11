
/****** 对象:  Table [dbo].[tbl_MallGoods]    脚本日期: 05/07/2015 08:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_MallGoods](
	[Id] [char](36) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[GoodsName] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[GoodsClassId] [int] NOT NULL,
	[StockNum] [int] NOT NULL,
	[SellNum] [int] NOT NULL,
	[IssueTime] [datetime] NOT NULL,
	[MarketPrice] [decimal](18, 2) NOT NULL,
	[MemberPrice] [decimal](18, 2) NOT NULL,
	[GoodsIntroduce] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[Status] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[OperatorId] [int] NOT NULL,
	[OperatorName] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NULL,
	[Producer] [nvarchar](150) COLLATE Chinese_PRC_CI_AS NULL,
	[Standard] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[StandardInfo] [nvarchar](150) COLLATE Chinese_PRC_CI_AS NULL,
	[GoodsPhoto] [nvarchar](250) COLLATE Chinese_PRC_CI_AS NULL,
	[IsGood] [bit] NOT NULL,
	[IsFreight] [bit] NOT NULL,
	[FreightFee] [decimal](18, 2) NULL,
 CONSTRAINT [PK_tbl_MallGoods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'GoodsName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品分类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'GoodsClassId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库存量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'StockNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已售量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'SellNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'IssueTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'市场价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'MarketPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'MemberPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'GoodsIntroduce'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态,1,在售,0,停售' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除，1：是，0：否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'OperatorId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'OperatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生成厂家' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'Producer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'Standard'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'GoodsPhoto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是精品商品，1：是，0：否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'IsGood'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否含运费，1：是，0，否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallGoods', @level2type=N'COLUMN',@level2name=N'IsFreight'


/****** 对象:  Table [dbo].[tbl_GoodsClass]    脚本日期: 05/07/2015 08:52:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_GoodsClass](
	[Id] [int] IDENTITY(2,1) NOT NULL,
	[ClassName] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[Type] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
 CONSTRAINT [PK_tbl_GoodsClass] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品分类名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_GoodsClass', @level2type=N'COLUMN',@level2name=N'ClassName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否有效，1：是，0：否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_GoodsClass', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识，1：商品分类，2：规格名称,3:规格类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_GoodsClass', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' 父Id,0:商品分类,1,规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_GoodsClass', @level2type=N'COLUMN',@level2name=N'ParentId'

/****** 对象:  Table [dbo].[tbl_ShoppingChart]    脚本日期: 05/07/2015 08:52:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ShoppingChart](
	[Id] [char](36) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[GoodsId] [char](36) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[GoodsName] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ShoppingNum] [int] NOT NULL,
	[PayType] [int] NULL,
	[JoinTime] [datetime] NOT NULL,
	[IsFreight] [bit] NOT NULL,
	[FreightFee] [decimal](18, 2) NULL,
	[GoodsFee] [decimal](18, 2) NOT NULL,
	[GoodsPhoto] [nvarchar](250) COLLATE Chinese_PRC_CI_AS NULL,
	[OrderId] [char](36) COLLATE Chinese_PRC_CI_AS NULL,
	[MemberId] [char](36) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[MemberName] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[NickName] [nvarchar](150) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_tbl_ShoppingChart] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_ShoppingChart', @level2type=N'COLUMN',@level2name=N'GoodsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_ShoppingChart', @level2type=N'COLUMN',@level2name=N'GoodsName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'选购数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_ShoppingChart', @level2type=N'COLUMN',@level2name=N'ShoppingNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式，1：微信支付' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_ShoppingChart', @level2type=N'COLUMN',@level2name=N'PayType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购买时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_ShoppingChart', @level2type=N'COLUMN',@level2name=N'JoinTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否含运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_ShoppingChart', @level2type=N'COLUMN',@level2name=N'IsFreight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_ShoppingChart', @level2type=N'COLUMN',@level2name=N'FreightFee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_ShoppingChart', @level2type=N'COLUMN',@level2name=N'GoodsFee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_ShoppingChart', @level2type=N'COLUMN',@level2name=N'OrderId'

/****** 对象:  Table [dbo].[tbl_MallOrder]    脚本日期: 05/08/2015 14:30:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_MallOrder](
	[OrderId] [char](36) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[OrderNo] [char](50) COLLATE Chinese_PRC_CI_AS NULL,
	[PayStatus] [int] NOT NULL,
	[AddressId] [char](36) COLLATE Chinese_PRC_CI_AS NULL,
	[MemberId] [char](36) COLLATE Chinese_PRC_CI_AS NULL,
	[MemberName] [nvarchar](150) COLLATE Chinese_PRC_CI_AS NULL,
	[MobilePhone] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[CreatTime] [datetime] NULL,
	[PayType] [int] NULL,
	[ReviewTime] [datetime] NULL,
	[CheckId] [int] NULL,
	[CheckName] [nvarchar](150) COLLATE Chinese_PRC_CI_AS NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_tbl_MallOrder] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallOrder', @level2type=N'COLUMN',@level2name=N'OrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态，1:未支付，2：已支付，3：无效，4：退订' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallOrder', @level2type=N'COLUMN',@level2name=N'PayStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物流编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallOrder', @level2type=N'COLUMN',@level2name=N'AddressId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下单人电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallOrder', @level2type=N'COLUMN',@level2name=N'MobilePhone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallOrder', @level2type=N'COLUMN',@level2name=N'ReviewTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallOrder', @level2type=N'COLUMN',@level2name=N'CheckId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_MallOrder', @level2type=N'COLUMN',@level2name=N'CheckName'

/****** 对象:  Table [dbo].[tbl_SendAddress]    脚本日期: 05/07/2015 08:53:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SendAddress](
	[Id] [char](36) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[LogisticsNo] [char](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Recipient] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[MobilePhone] [char](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Telephone] [char](50) COLLATE Chinese_PRC_CI_AS NULL,
	[CountyId] [int] NOT NULL,
	[CountyName] [nvarchar](250) COLLATE Chinese_PRC_CI_AS NULL,
	[ProvinceId] [int] NOT NULL,
	[ProvinceName] [nvarchar](250) COLLATE Chinese_PRC_CI_AS NULL,
	[CityId] [int] NOT NULL,
	[CityName] [nvarchar](250) COLLATE Chinese_PRC_CI_AS NULL,
	[AreaId] [int] NOT NULL,
	[AreaName] [nvarchar](250) COLLATE Chinese_PRC_CI_AS NULL,
	[Address] [nvarchar](250) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[IsDefaultAddress] [bit] NOT NULL,
	[MemberId] [char](36) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_tbl_SendAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物流编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_SendAddress', @level2type=N'COLUMN',@level2name=N'LogisticsNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_SendAddress', @level2type=N'COLUMN',@level2name=N'Recipient'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_SendAddress', @level2type=N'COLUMN',@level2name=N'MobilePhone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_SendAddress', @level2type=N'COLUMN',@level2name=N'Telephone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'详细地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_SendAddress', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否为默认地址，1：是，0：否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_SendAddress', @level2type=N'COLUMN',@level2name=N'IsDefaultAddress'

/****** 对象:  Table [dbo].[tbl_CodelModel]    脚本日期: 05/07/2015 10:29:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CodelModel](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[Codetype] [int] NOT NULL,
	[Codenum] [nvarchar](250) NOT NULL,
	[Codestate] [int] NOT NULL,
	[Usid] [nvarchar](250) NOT NULL,
	[Usnc] [nvarchar](250) NOT NULL,
	[Usname] [nvarchar](250) NOT NULL,
	[Ustel] [nvarchar](250) NOT NULL,
	[IsDelete] [int] NOT NULL,
 CONSTRAINT [PK_tbl_CodelModel] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'码分类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_CodelModel', @level2type=N'COLUMN',@level2name=N'Codetype'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'码编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_CodelModel', @level2type=N'COLUMN',@level2name=N'Codenum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'码状态 0未使用 1已使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_CodelModel', @level2type=N'COLUMN',@level2name=N'Codestate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_CodelModel', @level2type=N'COLUMN',@level2name=N'Usid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户昵称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_CodelModel', @level2type=N'COLUMN',@level2name=N'Usnc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_CodelModel', @level2type=N'COLUMN',@level2name=N'Usname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户手机号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_CodelModel', @level2type=N'COLUMN',@level2name=N'Ustel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:有效，0：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_CodelModel', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO

/****** 对象:  StoredProcedure [dbo].[esp_Code_Insert]    脚本日期: 05/08/2015 14:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[esp_Code_Insert] 
   @TourXml NVARCHAR(MAX)
   ,@RetCode INT OUTPUT--OUTPUT CODE
AS
BEGIN
	DECLARE @errorcount INT
	DECLARE @hdoc INT
	SET @errorcount=0
	BEGIN TRANSACTION
IF(@errorcount=0 AND @TourXml IS NOT NULL AND LEN(@TourXml)>0)
	BEGIN
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@TourXml
		INSERT INTO [tbl_CodelModel]([Codetype],[Codenum]
			,[Codestate],[Usid],[Usnc]
			,[Usname],[Ustel],[IsDelete])
		SELECT A.[Codetype],A.[Codenum]
			,A.[Codestate],A.[Usid],A.[Usnc]
			,A.[Usname],A.[Ustel],A.[IsDelete]
		FROM OPENXML(@hdoc,'/root/info')
		WITH([Codetype] INT,[Codenum] VARCHAR(255),[Codestate] INT,[Usid] VARCHAR(255),[Usnc] VARCHAR(255),[Usname] VARCHAR(255),[Ustel]VARCHAR(255),[IsDelete]INT) AS A
		SET @errorcount=@errorcount+@@ERROR
		EXECUTE sp_xml_removedocument @hdoc
	END
	IF(@errorcount<>0)
	BEGIN
		ROLLBACK TRAN
		SET @RetCode=-100
		RETURN @RetCode
	END

	COMMIT TRAN
	SET @RetCode=1
	RETURN @RetCode
END
GO
-----------------------------------------------------更新到以上截止-------------------------------------
/****** 对象:  View [dbo].[dt_MallGoodsView]    脚本日期: 05/08/2015 17:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[dt_MallGoodsView]
AS
SELECT  G.Id, G.GoodsName, G.GoodsClassId, G.Producer,G.Standard,G.StandardInfo, G.MemberPrice, G.MarketPrice, G.StockNum, G.GoodsIntroduce,G.GoodsPhoto,G.SellNum, G.IsGood,G.IsFreight,G.FreightFee,G.OperatorId, G.OperatorName, G.IssueTime, 
                      T.ClassName, G.Status, G.IsDelete 
                    
FROM         dbo.tbl_MallGoods AS G LEFT OUTER JOIN
            ( select Id,ClassName from  dbo.tbl_GoodsClass c where c.ParentId=0 and c.Type=1 and c.IsDelete=0) AS T ON G.GoodsClassId = T.Id 
GO
----------   ------------

/****** 对象:  Table [dbo].[tbl_Activity]    脚本日期: 05/08/2015 14:32:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Activity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Activitytypes] [int] NOT NULL,
	[Starname] [varchar](250) NOT NULL,
	[Title] [varchar](250) NOT NULL,
	[Address] [varchar](250) NOT NULL,
	[CostNum] [varchar](250) NOT NULL,
	[ActivityTxt] [text] NOT NULL,
	[Condition] [text] NOT NULL,
	[Innumber] [varchar](250) NOT NULL,
	[Rsdatetime] [datetime] NOT NULL,
	[Redatetime] [datetime] NOT NULL,
	[StartDatetime] [datetime] NOT NULL,
	[EndDatetime] [datetime] NOT NULL,
	[IsDelete] [int] NOT NULL,
	[UpUserId] [varchar](250) NOT NULL,
	[UpUserName] [varchar](250) NOT NULL,
	[UpDatetime] [datetime] NOT NULL,
	[CountryId] [int] NOT NULL,
	[Countryname] [varchar](250) NOT NULL,
	[ProvinceId] [int] NOT NULL,
	[Provincename] [varchar](250) NOT NULL,
	[CityId] [int] NOT NULL,
	[Cityname] [varchar](250) NOT NULL,
	[AreaId] [int] NOT NULL,
	[Areaname] [varchar](250) NOT NULL,
	[Cityyuzhan] [int] NOT NULL,
 CONSTRAINT [PK_tbl_Activity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动类型 枚举' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'Activitytypes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'球星' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'Starname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'CostNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'ActivityTxt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参与条件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'Condition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参加人数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'Innumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'报名开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'Rsdatetime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'报名结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'Redatetime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'StartDatetime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'EndDatetime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'逻辑删除  0已删除 1未删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'UpUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'UpUserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'UpDatetime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'CountryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'省' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'ProvinceId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'市' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'CityId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'AreaId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否同城  0否 1是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Activity', @level2type=N'COLUMN',@level2name=N'Cityyuzhan'

/****** 对象:  Table [dbo].[tbl_Applicants]    脚本日期: 05/08/2015 14:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Applicants](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Usid] [varchar](250) NOT NULL,
	[ActivityID] [varchar](250) NOT NULL,
	[Indatetime] [datetime] NOT NULL,
	[IsDelete] [int] NOT NULL,
 CONSTRAINT [PK_tbl_Applicants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Applicants', @level2type=N'COLUMN',@level2name=N'Usid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Applicants', @level2type=N'COLUMN',@level2name=N'ActivityID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'报名时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Applicants', @level2type=N'COLUMN',@level2name=N'Indatetime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'逻辑删除 0已删除1 未删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Applicants', @level2type=N'COLUMN',@level2name=N'IsDelete'

/****** 对象:  View [dbo].[dt_AcAlView]    脚本日期: 05/08/2015 14:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dt_AcAlView]
AS
SELECT     dbo.tbl_Activity.Id, dbo.tbl_Activity.Activitytypes, dbo.tbl_Activity.Starname, dbo.tbl_Activity.Title, dbo.tbl_Activity.Address, dbo.tbl_Activity.CostNum, 
                      dbo.tbl_Activity.ActivityTxt, dbo.tbl_Activity.Condition, dbo.tbl_Activity.Innumber, dbo.tbl_Activity.Rsdatetime, dbo.tbl_Activity.Redatetime, 
                      dbo.tbl_Activity.StartDatetime, dbo.tbl_Activity.EndDatetime, dbo.tbl_Activity.IsDelete, dbo.tbl_Activity.UpUserId, dbo.tbl_Activity.UpUserName, 
                      dbo.tbl_Activity.UpDatetime, dbo.tbl_Activity.CountryId, dbo.tbl_Activity.Countryname, dbo.tbl_Activity.ProvinceId, dbo.tbl_Activity.Provincename, 
                      dbo.tbl_Activity.CityId, dbo.tbl_Activity.Cityname, dbo.tbl_Activity.AreaId, dbo.tbl_Activity.Areaname, dbo.tbl_Activity.Cityyuzhan, 
                      dbo.tbl_Applicants.Usid, dbo.tbl_Applicants.ActivityID, dbo.tbl_Applicants.Indatetime, dbo.tbl_Applicants.IsDelete AS AlDelete, 
                      dbo.tbl_Applicants.Id AS AlId
FROM         dbo.tbl_Activity INNER JOIN
                      dbo.tbl_Applicants ON dbo.tbl_Activity.Id = dbo.tbl_Applicants.ActivityID

GO

/****** 对象:  View [dbo].[dt_ApplicantsMemberView]    脚本日期: 05/08/2015 14:35:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dt_ApplicantsMemberView]
AS
SELECT     dbo.tbl_Applicants.Usid, dbo.tbl_Applicants.ActivityID, dbo.tbl_Applicants.Indatetime, dbo.tbl_Member.Id, dbo.tbl_Member.TypeId, 
                      dbo.tbl_Member.CountryId, dbo.tbl_Member.CountryName, dbo.tbl_Member.ProvinceId, dbo.tbl_Member.ProvinceName, dbo.tbl_Member.CityId, 
                      dbo.tbl_Member.CityName, dbo.tbl_Member.AreaId, dbo.tbl_Member.AreaName, dbo.tbl_Member.UserName, dbo.tbl_Member.Password, 
                      dbo.tbl_Member.Title, dbo.tbl_Member.ContactName, dbo.tbl_Member.Gender, dbo.tbl_Member.MobilePhone, dbo.tbl_Member.Email, 
                      dbo.tbl_Member.PersonalId, dbo.tbl_Member.Address, dbo.tbl_Member.State, dbo.tbl_Member.LastModifyTime, dbo.tbl_Member.CheckTime, 
                      dbo.tbl_Member.CurrencyNumber, dbo.tbl_Member.IntegrationNumber, dbo.tbl_Member.HonorNumber, dbo.tbl_Member.IssueTime, 
                      dbo.tbl_Member.MemberPhoto, dbo.tbl_Member.NickName, dbo.tbl_Member.OpenId, dbo.tbl_Applicants.IsDelete
FROM         dbo.tbl_Applicants INNER JOIN
                      dbo.tbl_Member ON dbo.tbl_Applicants.Usid = dbo.tbl_Member.Id

GO

/****** 5月11日开始******/
/****** 对象:  [dbo].[tbl_Applicants]    脚本日期: 05/12/2015 13:30:23  ******/
alter table tbl_Applicants ADD Number [varchar](250) NULL ;
alter table tbl_Applicants ADD Contracttime [varchar](250) NULL ;
alter table tbl_Applicants ADD Remarks [text] NULL ;
alter table tbl_Applicants ADD Telephone [varchar](250) NULL ;
/****** 对象:  View [dbo].[dt_RegistrationView]    脚本日期: 05/11/2015 13:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dt_RegistrationView]
AS
SELECT     dbo.tbl_Member.ContactName, dbo.tbl_Member.CountryName, dbo.tbl_Member.ProvinceName, dbo.tbl_Member.CityName, dbo.tbl_Member.Address, 
                      dbo.tbl_Member.Email, dbo.tbl_Member.MobilePhone, dbo.tbl_Activity.Title, dbo.tbl_Applicants.Indatetime, dbo.tbl_Applicants.IsDelete, 
                      dbo.tbl_Activity.IsDelete AS ActIsDelete, dbo.tbl_Activity.Activitytypes, dbo.tbl_Applicants.Id, dbo.tbl_Member.AreaName, dbo.tbl_Activity.Release
FROM         dbo.tbl_Activity INNER JOIN
                      dbo.tbl_Applicants ON dbo.tbl_Activity.Id = dbo.tbl_Applicants.ActivityID INNER JOIN
                      dbo.tbl_Member ON dbo.tbl_Member.Id = dbo.tbl_Applicants.Usid

GO

/****** 对象:  [dbo].[tbl_Activity]    脚本日期: 05/11/2015 13:24:23 ******/
alter table tbl_Activity add Release INT NOT NULL DEFAULT 1;
                     
/****** 对象:  [dbo].[tbl_Applicants]    脚本日期: 05/11/2015 15:00:23 ******/   
alter table tbl_Applicants add IsState INT NOT NULL DEFAULT 0;
                        
/****** 对象:  [dbo].[tbl_Applicants]    脚本日期: 05/12/2015 8:30:23 ******/  
alter table tbl_Activity ADD SiteID [varchar](250)  NULL ;
alter table tbl_Activity ADD SiteName [varchar](250) NULL ;
GO
/****** 对象:  [dbo].[dt_AcAlView]    脚本日期: 05/12/2015 8:30:23  ******/
 ALTER VIEW dt_AcAlView  
AS
SELECT     dbo.tbl_Activity.Id, dbo.tbl_Activity.Activitytypes, dbo.tbl_Activity.Starname, dbo.tbl_Activity.Title, dbo.tbl_Activity.Address, dbo.tbl_Activity.CostNum, 
                      dbo.tbl_Activity.ActivityTxt, dbo.tbl_Activity.Condition, dbo.tbl_Activity.Innumber, dbo.tbl_Activity.Rsdatetime, dbo.tbl_Activity.Redatetime, 
                      dbo.tbl_Activity.StartDatetime, dbo.tbl_Activity.EndDatetime, dbo.tbl_Activity.IsDelete, dbo.tbl_Activity.UpUserId, dbo.tbl_Activity.UpUserName, 
                      dbo.tbl_Activity.UpDatetime, dbo.tbl_Activity.CountryId, dbo.tbl_Activity.Countryname, dbo.tbl_Activity.ProvinceId, dbo.tbl_Activity.Provincename, 
                      dbo.tbl_Activity.CityId, dbo.tbl_Activity.Cityname, dbo.tbl_Activity.AreaId, dbo.tbl_Activity.Areaname, dbo.tbl_Activity.Cityyuzhan, 
                      dbo.tbl_Applicants.Usid, dbo.tbl_Applicants.ActivityID, dbo.tbl_Applicants.Indatetime, dbo.tbl_Applicants.IsDelete AS AlDelete, 
                      dbo.tbl_Applicants.Id AS AlId, dbo.tbl_Activity.Release, dbo.tbl_Applicants.IsState, dbo.tbl_Activity.SiteID, dbo.tbl_Activity.SiteName
FROM         dbo.tbl_Activity INNER JOIN
                      dbo.tbl_Applicants ON CONVERT(varchar(100), dbo.tbl_Activity.Id) = dbo.tbl_Applicants.ActivityID
GO
/****** 对象:  [dbo].[dt_ApplicantsMemberView]    脚本日期: 05/12/2015 8:30:23  ******/
ALTER VIEW dt_ApplicantsMemberView  
AS
SELECT     dbo.tbl_Applicants.Usid, dbo.tbl_Applicants.ActivityID, dbo.tbl_Applicants.Indatetime, dbo.tbl_Member.Id, dbo.tbl_Member.TypeId, 
                      dbo.tbl_Member.CountryId, dbo.tbl_Member.CountryName, dbo.tbl_Member.ProvinceId, dbo.tbl_Member.ProvinceName, dbo.tbl_Member.CityId, 
                      dbo.tbl_Member.CityName, dbo.tbl_Member.AreaId, dbo.tbl_Member.AreaName, dbo.tbl_Member.UserName, dbo.tbl_Member.Password, 
                      dbo.tbl_Member.Title, dbo.tbl_Member.ContactName, dbo.tbl_Member.Gender, dbo.tbl_Member.MobilePhone, dbo.tbl_Member.Email, 
                      dbo.tbl_Member.PersonalId, dbo.tbl_Member.Address, dbo.tbl_Member.State, dbo.tbl_Member.LastModifyTime, dbo.tbl_Member.CheckTime, 
                      dbo.tbl_Member.CurrencyNumber, dbo.tbl_Member.IntegrationNumber, dbo.tbl_Member.HonorNumber, dbo.tbl_Member.IssueTime, 
                      dbo.tbl_Member.MemberPhoto, dbo.tbl_Member.NickName, dbo.tbl_Member.OpenId, dbo.tbl_Applicants.IsDelete, dbo.tbl_Applicants.IsState
FROM         dbo.tbl_Applicants INNER JOIN
                      dbo.tbl_Member ON dbo.tbl_Applicants.Usid = dbo.tbl_Member.Id
GO
/****** 对象:  [dbo].[dt_RegistrationView]    脚本日期: 05/12/2015 8:30:23  ******/
ALTER VIEW dt_RegistrationView 
AS
SELECT     dbo.tbl_Member.ContactName, dbo.tbl_Member.CountryName, dbo.tbl_Member.ProvinceName, dbo.tbl_Member.CityName, dbo.tbl_Member.Address, 
                      dbo.tbl_Member.Email, dbo.tbl_Member.MobilePhone, dbo.tbl_Activity.Title, dbo.tbl_Applicants.Indatetime, dbo.tbl_Applicants.IsDelete, 
                      dbo.tbl_Activity.IsDelete AS ActIsDelete, dbo.tbl_Activity.Activitytypes, dbo.tbl_Applicants.Id, dbo.tbl_Member.AreaName, dbo.tbl_Activity.Release, 
                      dbo.tbl_Applicants.IsState, dbo.tbl_Activity.SiteName, dbo.tbl_Activity.SiteID
FROM         dbo.tbl_Activity INNER JOIN
                      dbo.tbl_Applicants ON dbo.tbl_Applicants.ActivityID = CONVERT(varchar(100), dbo.tbl_Activity.Id) INNER JOIN
                      dbo.tbl_Member ON dbo.tbl_Member.Id = dbo.tbl_Applicants.Usid
GO
/****** 对象:  [dbo].[tbl_MallOrder]    脚本日期: 05/12/2015 13:30:23  ******/
alter table tbl_MallOrder ADD LogisticsId [varchar](250) NULL ;

/****** 对象:  [dbo].[tbl_SendAddress]    脚本日期: 05/12/2015 13:30:23  ******/
alter table tbl_SendAddress ADD IsDelete INT NULL ;

/****** 对象:  View [dbo].[dt_ApplicantsBallView]    脚本日期: 05/12/2015 16:10:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dt_ApplicantsBallView]
AS
SELECT     dbo.tbl_Applicants.Id, dbo.tbl_Applicants.Usid, dbo.tbl_Applicants.ActivityID, dbo.tbl_Applicants.Indatetime, dbo.tbl_Applicants.IsDelete, 
                      dbo.tbl_Applicants.IsState, dbo.tbl_Applicants.Number, dbo.tbl_Applicants.Contracttime, dbo.tbl_Applicants.Remarks, dbo.tbl_Applicants.Telephone, 
                      dbo.tbl_BallField.Id AS BallID, dbo.tbl_BallField.FieldType, dbo.tbl_BallField.UserId, dbo.tbl_BallField.ContactName, dbo.tbl_BallField.TypeId, 
                      dbo.tbl_BallField.CountryId, dbo.tbl_BallField.CountryName, dbo.tbl_BallField.ProvinceId, dbo.tbl_BallField.ProvinceName, dbo.tbl_BallField.CityId, 
                      dbo.tbl_BallField.CityName, dbo.tbl_BallField.CountyId, dbo.tbl_BallField.CountyName, dbo.tbl_BallField.FieldName, dbo.tbl_BallField.FieldNumber, 
                      dbo.tbl_BallField.FieldPhoto, dbo.tbl_BallField.IsDeleted, dbo.tbl_BallField.ShortName, dbo.tbl_BallField.IssueTime, dbo.tbl_BallField.State, 
                      dbo.tbl_BallField.OtherPhotoXml, dbo.tbl_BallField.Remark, dbo.tbl_BallField.FieldSize, dbo.tbl_BallField.Hours, dbo.tbl_BallField.Price, 
                      dbo.tbl_BallField.MarketPrice, dbo.tbl_BallField.ContactTel, dbo.tbl_BallField.Longitude, dbo.tbl_BallField.Latitude, dbo.tbl_BallField.Address
FROM         dbo.tbl_Applicants INNER JOIN
                      dbo.tbl_BallField ON dbo.tbl_Applicants.ActivityID = dbo.tbl_BallField.Id

GO

/****** 对象:  View [dbo].[dt_AppBallMenberView]    脚本日期: 05/13/2015 08:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dt_AppBallMenberView]
AS
SELECT     dbo.tbl_Applicants.Id AS AppId, dbo.tbl_Applicants.Telephone, dbo.tbl_Applicants.Remarks, dbo.tbl_Applicants.Contracttime, 
                      dbo.tbl_Applicants.Number, dbo.tbl_Applicants.IsState, dbo.tbl_Applicants.IsDelete, dbo.tbl_Applicants.Indatetime, dbo.tbl_Applicants.ActivityID, 
                      dbo.tbl_Applicants.Usid, dbo.tbl_BallField.FieldName, dbo.tbl_BallField.Id AS BallId, dbo.tbl_Member.Id AS MenberId, dbo.tbl_Member.ContactName, 
                      dbo.tbl_Member.MobilePhone, dbo.tbl_Member.Address, dbo.tbl_Member.Email, dbo.tbl_BallField.UserId AS UpUserID
FROM         dbo.tbl_Member INNER JOIN
                      dbo.tbl_Applicants ON dbo.tbl_Member.Id = dbo.tbl_Applicants.Usid INNER JOIN
                      dbo.tbl_BallField ON dbo.tbl_Applicants.ActivityID = dbo.tbl_BallField.Id

GO


/****** 对象:  Table [dbo].[tbl_Vote]    脚本日期: 05/14/2015 11:04:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Vote](
	[Vid] [nvarchar](250) NOT NULL,
	[Vtitle] [nvarchar](250) NOT NULL,
	[VRelease] [int] NOT NULL,
	[Vtype] [int] NOT NULL,
	[ExpireTime] [datetime] NOT NULL,
	[Remarks] [text] NULL,
	[SponsorId] [int] NOT NULL,
	[SponsorName] [nvarchar](250) NOT NULL,
	[LaunchTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_tbl_Vote] PRIMARY KEY CLUSTERED 
(
	[Vid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Vote', @level2type=N'COLUMN',@level2name=N'Vid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Vote', @level2type=N'COLUMN',@level2name=N'Vtitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布目标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Vote', @level2type=N'COLUMN',@level2name=N'VRelease'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投票类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Vote', @level2type=N'COLUMN',@level2name=N'Vtype'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'到期时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Vote', @level2type=N'COLUMN',@level2name=N'ExpireTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Vote', @level2type=N'COLUMN',@level2name=N'Remarks'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发起人名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Vote', @level2type=N'COLUMN',@level2name=N'SponsorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发起时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Vote', @level2type=N'COLUMN',@level2name=N'LaunchTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'逻辑删除  0可用 1不可用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Vote', @level2type=N'COLUMN',@level2name=N'IsDelete'

/****** 对象:  Table [dbo].[tbl_VoteOption]    脚本日期: 05/14/2015 11:05:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_VoteOption](
	[Oid] [varchar](250) NOT NULL,
	[VoteId] [varchar](250) NOT NULL,
	[Otitle] [varchar](250) NOT NULL,
	[ONumber] [int] NULL,
 CONSTRAINT [PK_tbl_OptionVote] PRIMARY KEY CLUSTERED 
(
	[Oid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投票表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_VoteOption', @level2type=N'COLUMN',@level2name=N'VoteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'选项名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_VoteOption', @level2type=N'COLUMN',@level2name=N'Otitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投票数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_VoteOption', @level2type=N'COLUMN',@level2name=N'ONumber'

/****** 对象:  Table [dbo].[tbl_VoteUser]    脚本日期: 05/14/2015 11:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_VoteUser](
	[Uid] [varchar](250) NOT NULL,
	[VoteOptionId] [varchar](250) NOT NULL,
	[UserId] [varchar](250) NOT NULL,
	[UIndatetime] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_VoteUser] PRIMARY KEY CLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投票选项ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_VoteUser', @level2type=N'COLUMN',@level2name=N'VoteOptionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投票时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_VoteUser', @level2type=N'COLUMN',@level2name=N'UIndatetime'

/****** 对象:  View [dbo].[dt_VoteUserView]    脚本日期: 05/14/2015 11:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dt_VoteUserView]
AS
SELECT     dbo.tbl_Vote.Vid, dbo.tbl_Vote.Vtitle, dbo.tbl_Vote.VRelease, dbo.tbl_Vote.Vtype, dbo.tbl_Vote.ExpireTime, dbo.tbl_Vote.Remarks, 
                      dbo.tbl_Vote.SponsorName, dbo.tbl_Vote.SponsorId, dbo.tbl_Vote.LaunchTime, dbo.tbl_Vote.IsDelete, dbo.tbl_VoteOption.Oid, 
                      dbo.tbl_VoteOption.VoteId, dbo.tbl_VoteOption.Otitle, dbo.tbl_VoteOption.ONumber, dbo.tbl_VoteUser.Uid, dbo.tbl_VoteUser.VoteOptionId, 
                      dbo.tbl_VoteUser.UserId, dbo.tbl_VoteUser.UIndatetime, dbo.tbl_Member.CountryName, dbo.tbl_Member.ProvinceName, dbo.tbl_Member.CityName, 
                      dbo.tbl_Member.AreaName, dbo.tbl_Member.ContactName, dbo.tbl_Member.Address, dbo.tbl_Member.MobilePhone, dbo.tbl_Member.Email
FROM         dbo.tbl_Vote INNER JOIN
                      dbo.tbl_VoteOption ON dbo.tbl_Vote.Vid = dbo.tbl_VoteOption.VoteId INNER JOIN
                      dbo.tbl_VoteUser ON dbo.tbl_VoteOption.Oid = dbo.tbl_VoteUser.VoteOptionId INNER JOIN
                      dbo.tbl_Member ON dbo.tbl_VoteUser.UserId = dbo.tbl_Member.Id

GO
/****** 5月11日结束******/

/******上面部分已更新 2015-5-13******/

/****** 5月14日下午开始******/

/****** 对象:  [dbo].[tbl_MallGoods]    脚本日期: 05/12/2015 13:30:23  ******/
alter table tbl_MallGoods ADD RoleClass INT NULL ;
alter table tbl_MallGoods ADD RoleClassName [nvarchar](250) NULL ;

/****** 对象:  Table [dbo].[tbl_GoodsRoleClass]    脚本日期: 05/14/2015 15:52:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_GoodsRoleClass](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rolename] [varchar](250) NOT NULL,
	[IsDelete] [int] NOT NULL,
	[GclassId] [int] NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_tbl_GoodsRoleClass] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
/****** 5月14日下午结束******/

/****** 5月15日上午开始******/
/****** 对象:  [dbo].[tbl_Vote]    脚本日期: 05/15/2015 08:33:23  ******/
alter table tbl_Vote ADD MatchName [varchar](250) NULL ;
alter table tbl_Vote ADD MatchId [varchar](250) NULL ;
/****** 对象:  [dbo].[tbl_VoteOption]    脚本日期: 05/15/2015 08:33:23  ******/
alter table tbl_VoteOption ADD OptIsDelete INT  NULL ;
/****** 对象:  [dbo].[tbl_VoteUser]    脚本日期: 05/15/2015 08:33:23  ******/
alter table tbl_VoteUser ADD VoteInfoId [varchar](250) NULL ;
alter table tbl_VoteUser ADD UsVoteIsDelete INT NULL ;
GO
/****** 对象:  [dbo].[dt_VoteUserView]    脚本日期: 05/15/2015 8:42:23  ******/
ALTER VIEW dt_VoteUserView  
AS
SELECT     dbo.tbl_Vote.Vid, dbo.tbl_Vote.Vtitle, dbo.tbl_Vote.VRelease, dbo.tbl_Vote.Vtype, dbo.tbl_Vote.ExpireTime, dbo.tbl_Vote.Remarks, 
                      dbo.tbl_Vote.SponsorName, dbo.tbl_Vote.SponsorId, dbo.tbl_Vote.LaunchTime, dbo.tbl_Vote.IsDelete, dbo.tbl_VoteOption.Oid, 
                      dbo.tbl_VoteOption.VoteId, dbo.tbl_VoteOption.Otitle, dbo.tbl_VoteOption.ONumber, dbo.tbl_VoteUser.Uid, dbo.tbl_VoteUser.VoteOptionId, 
                      dbo.tbl_VoteUser.UserId, dbo.tbl_VoteUser.UIndatetime, dbo.tbl_Member.CountryName, dbo.tbl_Member.ProvinceName, dbo.tbl_Member.CityName, 
                      dbo.tbl_Member.AreaName, dbo.tbl_Member.ContactName, dbo.tbl_Member.Address, dbo.tbl_Member.MobilePhone, dbo.tbl_Member.Email, 
                      dbo.tbl_Vote.MatchName, dbo.tbl_Vote.MatchId, dbo.tbl_VoteUser.VoteInfoId, dbo.tbl_VoteUser.UsVoteIsDelete, dbo.tbl_VoteOption.OptIsDelete
FROM         dbo.tbl_Vote INNER JOIN
                      dbo.tbl_VoteOption ON dbo.tbl_Vote.Vid = dbo.tbl_VoteOption.VoteId INNER JOIN
                      dbo.tbl_VoteUser ON dbo.tbl_VoteOption.Oid = dbo.tbl_VoteUser.VoteOptionId INNER JOIN
                      dbo.tbl_Member ON dbo.tbl_VoteUser.UserId = dbo.tbl_Member.Id
GO
/****** 5月15日下午开始******/
/******上面部分已更新 2015-5-16******/
/****** 5月18日上午开始******/
/****** 对象:  [dbo].[tbl_Vote]    脚本日期: 05/18/2015 09:30:23  ******/
alter table tbl_Vote ADD ColumnID INT DEFAULT 1;
alter table tbl_Vote ADD SubjectType INT NULL;
GO
UPDATE tbl_Vote SET ColumnID=1;
GO
/****** 对象:  [dbo].[tbl_VoteOption]    脚本日期: 05/18/2015 13:30:23  ******/
alter table tbl_VoteOption ADD SortNum INT NULL;
GO
UPDATE tbl_VoteOption SET SortNum=1;
GO

/****** 对象:  [dbo].[tbl_VoteUser]    脚本日期: 05/18/2015 16:30:23  ******/
alter table tbl_VoteUser ADD AwardsNum INT NULL ;
GO
UPDATE tbl_VoteUser SET AwardsNum=0;
GO
ALTER table tbl_VoteUser ALTER COLUMN [AwardsNum] INT NOT NULL; 
GO
/****** 对象:  [dt_VoteUserView]    脚本日期: 05/18/2015 09:30:23  ******/
ALTER VIEW dt_VoteUserView  
AS
SELECT     dbo.tbl_Vote.Vid, dbo.tbl_Vote.Vtitle, dbo.tbl_Vote.VRelease, dbo.tbl_Vote.Vtype, dbo.tbl_Vote.ExpireTime, dbo.tbl_Vote.Remarks, 
                      dbo.tbl_Vote.SponsorName, dbo.tbl_Vote.SponsorId, dbo.tbl_Vote.LaunchTime, dbo.tbl_Vote.IsDelete, dbo.tbl_VoteOption.Oid, 
                      dbo.tbl_VoteOption.VoteId, dbo.tbl_VoteOption.Otitle, dbo.tbl_VoteOption.ONumber, dbo.tbl_VoteUser.Uid, dbo.tbl_VoteUser.VoteOptionId, 
                      dbo.tbl_VoteUser.UserId, dbo.tbl_VoteUser.UIndatetime, dbo.tbl_Member.CountryName, dbo.tbl_Member.ProvinceName, dbo.tbl_Member.CityName, 
                      dbo.tbl_Member.AreaName, dbo.tbl_Member.ContactName, dbo.tbl_Member.Address, dbo.tbl_Member.MobilePhone, dbo.tbl_Member.Email, 
                      dbo.tbl_Vote.MatchName, dbo.tbl_Vote.MatchId, dbo.tbl_VoteUser.VoteInfoId, dbo.tbl_VoteUser.UsVoteIsDelete, dbo.tbl_VoteOption.OptIsDelete, 
                      dbo.tbl_Vote.ColumnID, dbo.tbl_Vote.SubjectType, dbo.tbl_VoteOption.SortNum, dbo.tbl_VoteUser.AwardsNum
FROM         dbo.tbl_Vote INNER JOIN
                      dbo.tbl_VoteOption ON dbo.tbl_Vote.Vid = dbo.tbl_VoteOption.VoteId INNER JOIN
                      dbo.tbl_VoteUser ON dbo.tbl_VoteOption.Oid = dbo.tbl_VoteUser.VoteOptionId INNER JOIN
                      dbo.tbl_Member ON dbo.tbl_VoteUser.UserId = dbo.tbl_Member.Id
GO
/****** 5月18日下午结束******/
/****** 5月22日上午开始******/
/****** 对象:  [dbo].[tbl_Vote]    脚本日期: 05/22/2015 16:30:23  ******/
alter table tbl_Vote ADD EstimateNum INT NULL ;
GO
UPDATE tbl_Vote SET EstimateNum=0;
GO
ALTER table tbl_Vote ALTER COLUMN [EstimateNum] INT NOT NULL; 
GO
/****** 5月22日下午结束******/
/****** 5月25日上午开始******/
/****** 对象:  Table [dbo].[tbl_AboutWar]    脚本日期: 05/25/2015 16:04:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_AboutWar](
	[Aid] [nvarchar](250) NOT NULL,
	[title] [nvarchar](250) NOT NULL,
	[MainID] [nvarchar](250) NOT NULL,
	[MainName] [nvarchar](250) NOT NULL,
	[GuestID] [nvarchar](250) NOT NULL,
	[GuestName] [nvarchar](250) NOT NULL,
	[Address] [nvarchar](250) NOT NULL,
	[CourtId] [nvarchar](250) NOT NULL,
	[CourtName] [nvarchar](250) NOT NULL,
	[AboutTime] [datetime] NOT NULL,
	[Atypes] [int] NOT NULL,
	[Format] [int] NOT NULL,
	[Costnum] [int] NOT NULL,
	[Afternoon] [text] NOT NULL,
	[AboutState] [int] NOT NULL,
	[Releasetime] [datetime] NOT NULL,
	[AIsDelete] [int] NOT NULL,
	[MainSnum] [nvarchar](250) NULL,
	[MainXnum] [nvarchar](250) NULL,
	[GuestSnum] [nvarchar](250) NULL,
	[GuestXnum] [nvarchar](250) NULL,
	[AWContent] [text] NULL,
	[Zbstate] [int] NOT NULL,
	[Uptime] [datetime] NULL,
 CONSTRAINT [PK_tbl_AboutWar] PRIMARY KEY CLUSTERED 
(
	[Aid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主队ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'MainID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主队名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'MainName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客队ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'GuestID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客队名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'GuestName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'球场ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'CourtId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'球场名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'CourtName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'约战时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'AboutTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'约战分类  1(一对多)  2(1对1)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'Atypes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'赛制' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'Format'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'Costnum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'战书' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'Afternoon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'约战状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'AboutState'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'Releasetime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'逻辑删除  0未删除 1 已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'AIsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主队伍上半场分数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'MainSnum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'战报详情' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'AWContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核状态 1待审核 2审核通过' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'Zbstate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'战报修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWar', @level2type=N'COLUMN',@level2name=N'Uptime'
/****** 对象:  Table [dbo].[tbl_AboutWarReport]    脚本日期: 05/25/2015 08:51:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_AboutWarReport](
	[Wid] [nvarchar](250) NOT NULL,
	[Wtypes] [int] NOT NULL,
	[TeamId] [nvarchar](250) NOT NULL,
	[Wstates] [int] NOT NULL,
	[AboutWarID] [nvarchar](250) NOT NULL,
	[Inserttime] [datetime] NOT NULL,
	[RIsDelete] [int] NOT NULL,
 CONSTRAINT [PK_tbl_AboutWarReport] PRIMARY KEY CLUSTERED 
(
	[Wid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'约战球队分类 1 主队 2客队' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWarReport', @level2type=N'COLUMN',@level2name=N'Wtypes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'球队ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWarReport', @level2type=N'COLUMN',@level2name=N'TeamId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWarReport', @level2type=N'COLUMN',@level2name=N'Wstates'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'报名约战时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWarReport', @level2type=N'COLUMN',@level2name=N'Inserttime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'逻辑删除  0未删除  1已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_AboutWarReport', @level2type=N'COLUMN',@level2name=N'RIsDelete'
GO
/****** 对象:  View [dbo].[dt_AboutWarBallTeam]    脚本日期: 05/25/2015 16:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dt_AboutWarBallTeam]
AS
SELECT     A.title, A.MainID, A.GuestID, A.Address, A.CourtId, A.CourtName, A.AboutTime, A.Format, A.Costnum, A.Afternoon, A.AboutState, A.Releasetime, 
                      A.AIsDelete, A.Aid, C.TypeId, C.MemberId, C.MemberName, C.CountryId, C.CountryName, C.ProvinceId, C.ProvinceName, C.CityId, C.CityName, 
                      C.AreaId, C.AreaName, C.TeamName, C.TeamPhoto, B.Wid, B.Wtypes, B.TeamId, B.Wstates, B.AboutWarID, B.Inserttime, B.RIsDelete, A.MainName, 
                      A.GuestName, A.Atypes, A.MainSnum, A.MainXnum, A.GuestSnum, A.GuestXnum, A.AWContent, A.Zbstate, A.Uptime
FROM         dbo.tbl_AboutWar AS A INNER JOIN
                      dbo.tbl_AboutWarReport AS B ON A.Aid = B.AboutWarID INNER JOIN
                      dbo.tbl_BallTeam AS C ON B.TeamId = C.Id

GO
/****** 5月25日下午结束******/
/****** 5月26日上午开始******/
/****** 对象:  [dbo].[tbl_Member]    脚本日期: 05/27/2015 13:30:23  ******/
alter table tbl_Member ADD Jobtyoe INT NULL ;
GO
/****** 对象:  [dbo].[tbl_Job]    脚本日期: 05/27/2015 13:30:23  ******/
alter table tbl_Job ADD IsDelete INT NULL ;
GO
UPDATE tbl_Job SET IsDelete=0;
GO
/****** 对象:  [dbo].[dt_MemberView]    脚本日期: 05/27/2015 8:30:23  ******/
ALTER VIEW dt_MemberView 
AS
SELECT     M.Id, M.TypeId, M.CountryId, M.CountryName, M.ProvinceId, M.ProvinceName, M.CityId, M.CityName, M.AreaId, M.AreaName, M.UserName, 
                      M.NickName, M.MemberPhoto AS HeadPhoto, M.Password, M.Title, M.ContactName, M.MobilePhone, M.Email, M.Gender, M.PersonalId, M.Address, 
                      M.State, M.LastModifyTime, M.CheckTime, M.CurrencyNumber, M.IntegrationNumber, M.HonorNumber, M.IssueTime, BT.TeamName, M.Jobtyoe
FROM         dbo.tbl_Member AS M LEFT OUTER JOIN
                      dbo.tbl_TeamMember AS TM ON M.Id = TM.MemberId LEFT OUTER JOIN
                      dbo.tbl_BallTeam AS BT ON TM.TeamId = BT.Id
GO
/****** 对象:  [dbo].[dt_JobList]    脚本日期: 05/27/2015 8:30:23  ******/
ALTER VIEW dt_JobList
AS
SELECT     J.Id, J.OperatorId, J.ContactName, J.TypeId, J.JobName, J.CoutryId, CO.Name AS CoutryName, J.ProvinceId, P.Name AS ProvinceName, J.CityId, 
                      C.Name AS CityName, A.CountyId, A.Name AS AreaName, J.StartDate, J.EndDate, J.JobNumber, J.JobRule, J.JobType, J.JobInfo, J.JobReward, 
                      J.IsValid, J.IssueTime, J.IsDelete
FROM         dbo.tbl_Job AS J LEFT OUTER JOIN
                      dbo.tbl_SysCountry AS CO ON J.CoutryId = CO.CountryId LEFT OUTER JOIN
                      dbo.tbl_SysProvince AS P ON J.ProvinceId = P.ProvinceId LEFT OUTER JOIN
                      dbo.tbl_SysCity AS C ON J.CityId = C.CityId LEFT OUTER JOIN
                      dbo.tbl_SysArea AS A ON J.AreaId = A.CountyId
GO
/****** 对象:  [dbo].[dt_MemberView]    脚本日期: 05/27/2015 8:30:23  ******/
ALTER VIEW dt_MemberView 
AS
SELECT     O.Id, O.JobId, J.JobName, O.MemberId, M.UserName, M.MobilePhone, M.ContactName, M.PersonalId, M.Address, M.Email, O.WorkYear, O.BallYear, 
                      O.Specialty, O.OfferState, O.IssueTime, O.BMGY, O.ApplyInfo, O.CountryId, O.CountryName, O.ProvinceName, O.ProvinceId, O.CityId, O.CityName, 
                      O.AreaId, O.AreaName, J.IsDelete
FROM         dbo.tbl_Offer AS O INNER JOIN
                      dbo.tbl_Job AS J ON O.JobId = J.Id INNER JOIN
                      dbo.tbl_Member AS M ON O.MemberId = M.Id
GO
/****** 对象:  View [dbo].[dt_OfferMemberView]    脚本日期: 05/27/2015 17:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dt_OfferMemberView]
AS
SELECT     M.CountryId, M.CountryName, M.ProvinceId, M.ProvinceName, M.CityId, M.CityName, M.AreaId, M.AreaName, M.UserName, M.ContactName, 
                      M.MobilePhone, M.Email, M.Address, O.Id AS OfId, O.JobId, O.MemberId, O.WorkYear, O.BallYear, O.Specialty, O.BMGY, O.ApplyInfo, O.OfferState, 
                      O.IssueTime, O.CountryId AS OfCountryId, O.CountryName AS OfCountryName, O.ProvinceId AS OfProvinceId, O.ProvinceName AS OfProvinceName, 
                      O.CityId AS OfCityId, O.CityName AS OfCityName, O.AreaId AS OfAreaId, O.AreaName AS OfAreaName, M.Id, M.MemberPhoto,
                          (SELECT     CSWZ
                            FROM          dbo.tbl_MemberInterest AS m
                            WHERE      (MemberId = O.MemberId)) AS CSWZ, M.Jobtyoe, M.Gender
FROM         dbo.tbl_Offer AS O INNER JOIN
                      dbo.tbl_Member AS M ON O.MemberId = M.Id

GO
/****** 5月26日下午结束******/
/****** 5月28日上午开始******/
/****** 对象:  Table [dbo].[tbl_Offerpat]    脚本日期: 05/28/2015 17:30:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Offerpat](
	[Id] [nvarchar](250) NOT NULL,
	[Pattype] [int] NOT NULL,
	[PatId] [nvarchar](250) NOT NULL,
	[MemberId] [nvarchar](250) NOT NULL,
	[Inserttime] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_Offerpat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  View [dbo].[dt_OfferpatBallTeamView]    脚本日期: 05/28/2015 17:31:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dt_OfferpatBallTeamView]
AS
SELECT     B.Id, B.TypeId, B.MemberId, B.MemberName, B.CountryId, B.CountryName, B.ProvinceId, B.ProvinceName, B.CityId, B.CityName, B.AreaId, 
                      B.AreaName, B.TeamName, B.TeamPhoto, B.Badge, B.TeamInfo, B.State, B.HonorNumber, B.CheckId, B.CheckName, B.CheckTime, B.EndCheckId, 
                      B.EndCheckName, B.EndCheckTime, B.RequestTime, B.RequestCheckId, B.RequestCheckName, B.RequestCheckTime, B.IssueTime, 
                      B.Refuse_check_reason, B.DepositTotalMoney, B.Refuse_encheck_reason, B.DisbandReason, B.Refuse_disband_reason, B.IsImport, 
                      O.Id AS OfferpatId, O.Pattype, O.PatId, O.MemberId AS OfferpatMemberId, O.Inserttime
FROM         dbo.tbl_BallTeam AS B INNER JOIN
                      dbo.tbl_Offerpat AS O ON O.PatId = B.Id

GO
/****** 对象:  View [dbo].[dt_PatOfferMemberView]    脚本日期: 05/28/2015 17:31:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dt_PatOfferMemberView]
AS
SELECT     M.CountryId, M.CountryName, M.ProvinceId, M.ProvinceName, M.CityId, M.CityName, M.AreaId, M.AreaName, M.UserName, M.ContactName, 
                      M.MobilePhone, M.Email, M.Address, O.Id AS OfId, O.JobId, O.MemberId, O.WorkYear, O.BallYear, O.Specialty, O.BMGY, O.ApplyInfo, O.OfferState, 
                      O.IssueTime, O.CountryId AS OfCountryId, O.CountryName AS OfCountryName, O.ProvinceId AS OfProvinceId, O.ProvinceName AS OfProvinceName, 
                      O.CityId AS OfCityId, O.CityName AS OfCityName, O.AreaId AS OfAreaId, O.AreaName AS OfAreaName, M.Id, M.MemberPhoto,
                          (SELECT     CSWZ
                            FROM          dbo.tbl_MemberInterest AS m
                            WHERE      (MemberId = O.MemberId)) AS CSWZ, M.Jobtyoe, M.Gender, P.Pattype, P.PatId, P.MemberId AS OfferpatMemberId, P.Inserttime, 
                      P.Id AS OfferpatID
FROM         dbo.tbl_Offer AS O INNER JOIN
                      dbo.tbl_Member AS M ON O.MemberId = M.Id INNER JOIN
                      dbo.tbl_Offerpat AS P ON P.PatId = M.Id

GO
/****** 5月28日下午结束******/
/******* 截止5月28日 以上已更新 *****/
 




/****** 6月1日开始******/
/****** 对象:  [dbo].[tbl_AboutWar]    脚本日期: 06/1/2015 16:30:23  ******/
alter table tbl_AboutWar ADD AwcityId INT NULL ;
GO
UPDATE [dbo].[tbl_AboutWar]
   SET AwcityId =(SELECT CityId FROM tbl_BallField AS BF WHERE tbl_AboutWar.CourtId=bf.Id)
GO
ALTER table tbl_AboutWar ALTER COLUMN [AwcityId] INT NOT NULL; 
GO
/****** 对象:  View [dbo].[dt_AboutWarBallTeam]    脚本日期: 06/1/2015 17:00:48 ******/
ALTER VIEW [dbo].[dt_AboutWarBallTeam]
AS
SELECT     A.title, A.MainID, A.GuestID, A.Address, A.CourtId, A.CourtName, A.AboutTime, A.Format, A.Costnum, A.Afternoon, A.AboutState, A.Releasetime, 
                      A.AIsDelete, A.Aid, C.TypeId, C.MemberId, C.MemberName, C.CountryId, C.CountryName, C.ProvinceId, C.ProvinceName, C.CityId, C.CityName, 
                      C.AreaId, C.AreaName, C.TeamName, C.TeamPhoto, B.Wid, B.Wtypes, B.TeamId, B.Wstates, B.AboutWarID, B.Inserttime, B.RIsDelete, A.MainName, 
                      A.GuestName, A.Atypes, A.MainSnum, A.MainXnum, A.GuestSnum, A.GuestXnum, A.AWContent, A.Zbstate, A.Uptime, A.AwcityId
FROM         dbo.tbl_AboutWar AS A INNER JOIN
                      dbo.tbl_AboutWarReport AS B ON A.Aid = B.AboutWarID INNER JOIN
                      dbo.tbl_BallTeam AS C ON B.TeamId = C.Id
GO
/****** 6月1日结束******/
/****** 6月2日开始******/
/****** 对象:  View [dbo].[dt_BallTeamTimeView]    脚本日期: 06/02/2015 11:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dt_BallTeamTimeView]
AS
SELECT     TOP (100) PERCENT Id, TypeId, MemberId, MemberName, CountryId, CountryName, ProvinceId, ProvinceName, CityId, CityName, AreaId, AreaName, 
                      TeamName, TeamPhoto, Badge, TeamInfo, State, HonorNumber, CheckId, CheckName, CheckTime, EndCheckId, EndCheckName, EndCheckTime, 
                      RequestTime, RequestCheckId, RequestCheckName, RequestCheckTime, IssueTime, DepositTotalMoney, Refuse_check_reason, 
                      Refuse_encheck_reason, DisbandReason, Refuse_disband_reason, IsImport,
                          (SELECT     TOP (1) IssueTime
                            FROM          dbo.tbl_Articles AS A
                            WHERE      (TeamId = dbo.tbl_BallTeam.Id)
                            ORDER BY IssueTime DESC) AS tjtime
FROM         dbo.tbl_BallTeam

GO
/****** 对象:  View [dbo].[dt_AboutWarBallTeam]    脚本日期: 06/2/2015 13:00:48 ******/
ALTER VIEW [dbo].[dt_AboutWarBallTeam]
AS
SELECT     A.title, A.MainID, A.GuestID, A.Address, A.CourtId, A.CourtName, A.AboutTime, A.Format, A.Costnum, A.Afternoon, A.AboutState, A.Releasetime, 
                      A.AIsDelete, A.Aid, C.TypeId, C.MemberId, C.MemberName, C.CountryId, C.CountryName, C.ProvinceId, C.ProvinceName, C.CityId, C.CityName, 
                      C.AreaId, C.AreaName, C.TeamName, C.TeamPhoto, B.Wid, B.Wtypes, B.TeamId, B.Wstates, B.AboutWarID, B.Inserttime, B.RIsDelete, A.MainName, 
                      A.GuestName, A.Atypes, A.MainSnum, A.MainXnum, A.GuestSnum, A.GuestXnum, A.AWContent, A.Zbstate, A.Uptime, A.AwcityId
FROM         dbo.tbl_AboutWar AS A INNER JOIN
                      dbo.tbl_AboutWarReport AS B ON A.Aid = B.AboutWarID INNER JOIN
                      dbo.tbl_BallTeam AS C ON B.TeamId = C.Id
GO
/****** 对象:  View [dbo].[dt_MembertdView]    脚本日期: 06/02/2015 13:41:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dt_MembertdView]
AS
SELECT     M.Id, M.TypeId, M.CountryId, M.CountryName, M.ProvinceId, M.ProvinceName, M.CityId, M.CityName, M.AreaId, M.AreaName, M.UserName, 
                      M.NickName, M.MemberPhoto AS HeadPhoto, M.Password, M.Title, M.ContactName, M.MobilePhone, M.Email, M.Gender, M.PersonalId, M.Address, 
                      M.State, M.LastModifyTime, M.CheckTime, M.CurrencyNumber, M.IntegrationNumber, M.HonorNumber, M.IssueTime, BT.TeamName, M.Jobtyoe
FROM         dbo.tbl_Member AS M LEFT OUTER JOIN
                      dbo.tbl_TeamMember AS TM ON M.Id = TM.MemberId LEFT OUTER JOIN
                      dbo.tbl_BallTeam AS BT ON TM.TeamId = BT.Id

GO
/****** 6月2日结束******/
/******6月2日最新******/
/****** 对象:  View [dbo].[dt_AboutWarBallTeam]    脚本日期: 06/2/2015 13:00:48 ******/
ALTER VIEW [dbo].[dt_AboutWarBallTeam]
AS
SELECT     A.title, A.MainID, A.GuestID, A.Address, A.CourtId, A.CourtName, A.AboutTime, A.Format, A.Costnum, A.Afternoon, A.AboutState, A.Releasetime, 
                      A.AIsDelete, A.Aid, C.TypeId, C.MemberId, C.MemberName, C.CountryId, C.CountryName, C.ProvinceId, C.ProvinceName, C.CityId, C.CityName, 
                      C.AreaId, C.AreaName, C.TeamName, C.TeamPhoto, B.Wid, B.Wtypes, B.TeamId, B.Wstates, B.AboutWarID, B.Inserttime, B.RIsDelete, A.MainName, 
                      A.GuestName, A.Atypes, A.MainSnum, A.MainXnum, A.GuestSnum, A.GuestXnum, A.AWContent, A.Zbstate, A.Uptime, A.AwcityId
FROM         dbo.tbl_AboutWar AS A INNER JOIN
                      dbo.tbl_AboutWarReport AS B ON A.Aid = B.AboutWarID INNER JOIN
                      dbo.tbl_BallTeam AS C ON B.TeamId = C.Id
GO
/******6月2日最新******/
/****** 以上已更新 ******/
/******6月8日-6月9日******/
/****** 对象:  View [dbo].[tbl_MallGoods]    脚本日期: 06/8/2015 9:00:48 ******/
alter table tbl_MallGoods ADD MemberId nvarchar(250) NULL ;
GO
/****** 对象:  View [dbo].[dt_BazaarOrderView]    脚本日期: 06/09/2015 16:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dt_BazaarOrderView]
AS
SELECT     MO.OrderId, MO.OrderNo, MO.PayStatus, MO.AddressId, MO.MemberId, MO.MemberName, MO.MobilePhone, MO.CreatTime, MO.PayType, 
                      MO.ReviewTime, MO.CheckName, MO.CheckId, MO.IsDelete, MO.LogisticsId, MG.Id, MG.GoodsName, MG.GoodsClassId, MG.StockNum, MG.SellNum, 
                      MG.IssueTime, MG.MarketPrice, MG.MemberPrice, MG.GoodsIntroduce, MG.Status, MG.IsDelete AS GoodsIsDelete, MG.OperatorId, MG.OperatorName, 
                      MG.Producer, MG.Standard, MG.StandardInfo, MG.GoodsPhoto, MG.IsGood, MG.IsFreight, MG.FreightFee, MG.RoleClass, MG.RoleClassName, 
                      MG.MemberId AS GoodsMemberId, SC.ShoppingNum, SC.GoodsId, SC.GoodsName AS CharGoodName, SC.PayType AS CharPayType, SC.JoinTime, 
                      SC.IsFreight AS CharIsFreight, SC.FreightFee AS CharFreightFee, SC.GoodsFee, SC.GoodsPhoto AS CharGoodsPhoto, SC.NickName, 
                      SC.MemberName AS CharMemberName, SC.MemberId AS CharMemberId
FROM         dbo.tbl_MallOrder AS MO INNER JOIN
                      dbo.tbl_ShoppingChart AS SC ON MO.OrderId = SC.OrderId INNER JOIN
                      dbo.tbl_MallGoods AS MG ON MG.Id = SC.GoodsId

GO
/******6月8日-6月9日******/