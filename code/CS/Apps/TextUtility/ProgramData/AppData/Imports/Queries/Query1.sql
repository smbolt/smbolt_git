SELECT DISTINCT W.WellName, W.GPWellNo, W.Active, W.Operated, 
                SC.CountyName, ST.StateName, F.FieldName, W.API
  FROM [GPMaster].[dbo].[Well] W
  LEFT JOIN GPMaster.dbo.WellLocation WL ON WL.WellId = W.WellID
  LEFT JOIN GPMaster.dbo.StateCounty SC ON WL.StateCountyID = SC.StateCountyID
  LEFT JOIN GPMaster.dbo.StateRef ST ON SC.StateID = ST.StateID
  LEFT JOIN GPMaster.dbo.Field F ON F.FieldID = WL.FieldID
  WHERE W.Active = 'Y'
    AND W.Operated = 'Y'
    AND SC.StateID = 38
    AND (W.GPWellNo IN
          (  
            SELECT DISTINCT(GPWellNo) 
            FROM GPIntegration.GP.v_c_AllocatedDailyWellProduction
          )
        )
  ORDER BY W.WellName