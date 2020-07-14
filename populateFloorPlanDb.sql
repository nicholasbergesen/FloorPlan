select * 
into PropertySearch
from [PTLDB2016].[P24Master].[dbo].[PropertySearch]

ALTER TABLE [dbo].[PropertySearch] ADD  CONSTRAINT [PK_MstPropertySearch] PRIMARY KEY CLUSTERED 
(
	[MandateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

select a.* 
into MandateImage
from [PTLDB2016].[P24Master].[dbo].MandateImage a 
inner join PropertySearch b on a.MandateId = b.MandateId

ALTER TABLE [dbo].[MandateImage] ADD  CONSTRAINT [PK_PropertyId_Ordinal] PRIMARY KEY CLUSTERED 
(
	[MandateId] ASC,
	[Ordinal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

select a.* 
into ImageReferenceIdToImageId
from [PTLDB2016].[P24Master].[dbo].ImageReferenceIdToImageId a 
inner join MandateImage b on a.ReferenceId = b.ImageReferenceId

ALTER TABLE [dbo].[ImageReferenceIdToImageId] ADD  CONSTRAINT [PK_ImageReferenceIdToImageId__ReferenceId] PRIMARY KEY CLUSTERED 
(
	[ReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

select * 
into Development
from [PTLDB2016].[P24Master].[dbo].[Development]

CREATE nonclustered INDEX IX_ImageID ON ImageReferenceIdToImageId
(
	ImageId asc
) Include (Confidence)

select * from ImageReferenceIdToImageId where OldProcess = 0 and Confidence is not null