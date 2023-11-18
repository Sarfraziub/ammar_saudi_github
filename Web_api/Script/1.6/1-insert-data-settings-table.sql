
--After Migration
INSERT INTO [dbo].[Settings] ([Key],[Value],[Created],[CreatedBy],[Updated],[UpdatedBy],[Active]) VALUES('WhatsAppSettings.InstanceId','653131F7E25A4',GETDATE(),00966500825442,GETDATE(),00966500825442,1)
INSERT INTO [dbo].[Settings] ([Key],[Value],[Created],[CreatedBy],[Updated],[UpdatedBy],[Active]) VALUES('WhatsAppSettings.AccessToken','652558fb3a74f',GETDATE(),00966500825442,GETDATE(),00966500825442,1)



INSERT INTO [dbo].[ContentSettings]([Key],[Title],[Content],[ArabicContent],[Created],[CreatedBy],[Updated],[UpdatedBy],[Active])
     VALUES
           ('WhatsApp.PaymentDoneMessage'
		   ,'WhatsApp.PaymentDoneMessage'
           ,'Payment successful with oreder id : <<OrderNumber>>'
           ,N'  أهلا وسهلا بك ..  تم اعتماد طلبك رقم:  <<OrderNumber>>  وسنسعى لتوصيله في أقرب وقت ممكن ⏳  كتب الله أجرك. '
           ,GETDATE()
           ,'00966500825442'
           ,GETDATE()
           ,'00966500825442'
           ,1);


INSERT INTO [dbo].[ContentSettings]([Key],[Title],[Content],[ArabicContent],[Created],[CreatedBy],[Updated],[UpdatedBy],[Active])
     VALUES
           ('WhatsApp.DeliveryDoneMessage'
		   ,'WhatsApp.DeliveryDoneMessage'
           ,'Delivery successful with oreder id : <<OrderNumber>>'
           ,N'تم توصيل طلبك رقم:  <<OrderNumber>> 
بإمكانك الاطلاع على صور التوصيل عبر التطبيق 📲

تشرفنا بك كثير ونحن نسعد بخدمتك، شكراً لاختيارك وثقتك بقطرات 🌹
'
           ,GETDATE()
           ,'00966500825442'
           ,GETDATE()
           ,'00966500825442'
           ,1)
		   
INSERT INTO [dbo].[ContentSettings]([Key],[Title],[Content],[ArabicContent],[Created],[CreatedBy],[Updated],[UpdatedBy],[Active])
     VALUES
           ('WhatsApp.SendGiftMessage'
		   ,'WhatsApp.SendGiftMessage'
           ,'Delivery successful with oreder id : <<OrderNumber>>'
           ,N'إلى: ( <<ReceiverName>> )
		   
محبةً لك، ساهمت عنك في سقيا ماء لمساجد مكة المكرمة عبر تطبيق قطرات ..

من: ( <<SenderName>> )'
           ,GETDATE()
           ,'00966500825442'
           ,GETDATE()
           ,'00966500825442'
           ,1)
		   
insert into SiteConfigurations(AndroidAppVersion, IosAppVersion, IsMaintenanceMode,ForceUpdate, Active, Created) values('10.1','5.1',0,0,1,GETDATE())