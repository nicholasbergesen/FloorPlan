--0.5 0.7

	SELECT distinct 'https://images.prop24.com/' + convert(varchar(20), imageId) [Url], Confidence
  FROM [FloorPlan].[dbo].[ImageReferenceIdToImageId]
  where confidence <> 100 and OldProcess = 0 and confidence > 0.7
  order by Confidence asc, [Url]

  	SELECT distinct 'https://images.prop24.com/' + convert(varchar(20), imageId) [Url], Confidence
  FROM [FloorPlan].[dbo].[ImageReferenceIdToImageId]
  where confidence <> 100 and Confidence is not null
  order by Confidence asc, [Url]

select count(*) from ImageReferenceIdToImageId where OldProcess = 1 and Confidence is not null 

update ImageReferenceIdToImageId set OldProcess = 1 where Confidence is not null and OldProcess <> 1
update [ImageReferenceIdToImageId] set Confidence = null, OldProcess = 0
update [ImageReferenceIdToImageId] set OldProcess = 1 where Confidence is not null

select count(d.ImageId) from Development a 
inner join PropertySearch b on b.DevelopmentId = a.Id
inner join MandateImage c on c.MandateId = b.MandateId
inner join ImageReferenceIdToImageId d on d.ReferenceId = c.ImageReferenceId
where Confidence is null

update top (10000) ImageReferenceIdToImageId set Confidence = 100

select count(*) from ImageReferenceIdToImageId where Confidence is not null