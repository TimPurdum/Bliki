<!-- TITLE: IBR V3 Readme -->
<!-- SUBTITLE: Copy of Readme from Master Solution 5.0. Any edits should be kept in sync. -->

# IBR V3 (2018) Implementation Readme

## Error Codes
__The first number of each code (unless it's a zero) refers to the segment__

### Report Level Errors
__{050}__ `IbrTransformer.Transform`  
Segment Levels in a Group A Incident Report must be organized
 in numerical order. For example, an incident having segments
 1, 2, 2, 3, 4, 4, 4, 5 must
be written in that order, not as 1, 2, 2, 5, 3, 4, 4, 4.

__{051}__ - *Handled by formatters*  
Segment Level must contain data values 0–7.

__{055}__ `IbrTransformer.Transform` - *Handled by transformer*    
Segment Level 1 (Administrative Segment) with Segment Action
 Type I = Incident Report must be the first segment submitted
 for each Group A Incident Report.

__{056}__ - *Let fail*  
Data Element 2 (Incident Number) must be a unique number for
 each incident submitted. No two incidents can have the same
 incident number.

__{058}__ - *Handled by formatters*  
Month of Submission and Year of Submission must contain the
 same data values for each segment in a NIBRS submission.
 The first segment processed will be compared with all other
 segments to check for this condition.

__{059}__ - *Let fail*  
Data Element 1 (ORI) must contain the same state abbreviation
 code (e.g., SC, MD, etc.) in the first two positions (record
 positions 17 & 18). For nonfederal LEAs, every segment in a
 submission must have the same state code in the first two
 positions of the ORI.

__{060}__ `SubmissionMonthSubmissionValidationRule.SubmissionIsAfterCurrentMonth`  
Month of Submission and Year of Submission must precede the
 date the FBI receives and processes a NIBRS submission.
 This edit checks for data submitted for a future month/year.

__{065}__ `IbrTransformer.BuildOffenseRecords`, `OffenseUcrCodeValidationRule.HasReportableVictims`  
Segment Level 2 (Offense Segment) must have at least one
 Segment Level 4 (Victim Segment) connected to it by
 entering the offense code identified in Data Element 6
 (UCR Offense Code) in Data Element 24 (Victim Connected
 to UCR Offense Code).

__{070}__ - *Handled by UI*  
Data Element 34 (Offender Numbers To Be Related) has a value
 that does not have a corresponding Offender Segment. For
 example, if the field value shown in Data Element 34 is 15,
 an Offender Segment does not exist with a value of 15 in
 Data Element 36 (Offender Sequence Number).

__{071}__ `AdminExceptionalClearanceValidationRule.ArrestDateIsAfterClearance`  
Segment Level 6 (Arrestee Segment) with Segment Action Type
 I = Incident Report cannot be submitted with Data Element
 42 (Arrest Date) containing an arrest date on or earlier
 than the date entered in Data Element 5 (Exceptional
 Clearance Date) when Data Element 4 (Cleared Exceptionally)
 contains a data value other than N = Not Applicable
 (indicating the incident is cleared exceptionally).

__{072}__ `IbrTransformer.BuildPropertyRecords`  
Segment Level 3 (Property Segment) must first be submitted 
with Data Element 14 (Type Property Loss/Etc.) as 7 = 
Stolen/Etc. before it can be submitted as 5 = Recovered 
for the same property in Data Element 15 (Property 
Description). Any property being reported as recovered 
must first be reported as stolen.
There are exceptions to this rule:
1) When Data Element 6 (UCR Offense Code) contains an offense that allows property to be recovered without first being stolen in that same incident (i.e., 250 = Counterfeiting/Forgery and 280 = Stolen Property Offenses)
2) When a vehicle was stolen and the recovered property in Data Element 15 (Property Description) is 38 = Vehicle Parts/Accessories

__{073}__ `IbrTransformer.GetImpliedVehicles`  
Segment Level 3 (Property Segment) with
Segment Action Type I = Incident Report must
contain a data value in Data Element 18 (Number
of Stolen Motor Vehicles) greater than or equal to
the data value entered in Data Element 19
(Number of Recovered Motor Vehicles) within the
same incident.

__{074}__ `OffenseUcrCodeValidationRule.RequiresProperty`  
Segment Level 3 (Property Segment) with
Segment Action Type I = Incident Report must be
submitted when Data Element 6 (UCR Offense
Code) contains an offense of
Kidnapping/Abduction , Crimes Against Property,
Drug/Narcotic Offenses, or Gambling Offenses.

__{075}__ `IbrTransformer.TransformGroupAIncident`  
Segment Levels 1, 2, 4, and 5 (Administrative
Segment, Offense Segment, Victim Segment, and
Offender Segment) with Segment Action Type I =
Incident Report must be submitted for each Group
A Incident Report; they are mandatory.

__{076}__ `PropertyOffenseLinkValidationRule.Validate`  
Segment Level 3 (Property Segment) with
Segment Action Type I = Incident Report cannot be
submitted unless Data Element 6 (UCR Offense
Code) contains an offense of
Kidnapping/Abduction, Crimes Against Property,
Drug/Narcotic Offenses, or Gambling Offenses.

__{077}__ `PropertyOffenseLinkValidationRule.Validate`  
Data Element 7 (Offense Attempted/Completed) is
A = Attempted and Data Element 6 (UCR Offense
Code) is a Crime Against Property, Gambling,
Kidnapping, or Drug/Narcotic Offense. However,
there is no Data Element 14 (Type Property
Loss/Etc.) of 1 = None or 8 = Unknown.

__{078}__ `PropertyOffenseLinkValidationRule.Validate`  
If Data Element 6 (UCR Offense Code) is a Crime
Against Property, Kidnaping, Gambling, or
Drug/Narcotic Offense, and Data Element 7
(Offense Attempted/Completed) is C = Completed,
a Property Segment (Level 3) must be sent with a
valid code in Data Element 14 (Type Property
Loss/Etc.).

__{080}__ `VictimOffenseLinkValidationRule.Validate`  
Segment Level 4 (Victim Segment) can be
submitted only once and Data Element 25 (Type of
Victim) must be S = Society/Public when Data
Element 6 (UCR Offense Code) contains only a
Crime Against Society.

__{081}-1__ `PropertyOffenseLinkValidationRule.Validate`  
Data Element 14 (Type Property Loss/Etc.) must
be 1 = None or 8 = Unknown when Data Element 6
(UCR Offense Code) contains an offense of
Kidnapping/Abduction, Crimes against Property,
Drug/Narcotic Offenses, or Gambling Offenses and
Data Element 7 (Offense Attempted/Completed) is
A = Attempted.

__{081}-2__ `PropertyOffenseLinkValidationRule.Validate`  
Data Element 14 (Type Property Loss/Etc.) must
be 1 = None or 5 = Recovered when Data Element
6 (UCR Offense Code) is 280 = Stolen Property
Offenses and Data Element 7 (Offense
Attempted/Completed) is C = Completed.

__{081}-3__ `PropertyOffenseLinkValidationRule.Validate`  
Data Element 14 (Type Property Loss/Etc.) must
be 1 = None, 5 = Recovered, 7 = Stolen/Etc., or 8
= Unknown when Data Element 6 (UCR Offense
Code) is 100 = Kidnapping/Abduction, 220 =
Burglary/ Breaking & Entering, or 510 = Bribery
and Data Element 7 (Offense
Attempted/Completed) is C = Completed.

__{081}-4__ `PropertyOffenseLinkValidationRule.Validate`  
Data Element 14 (Type Property Loss/Etc.) must
be 1 = None or 6 = Seized when Data Element 6
(UCR Offense Code) is 35A = Drug/ Narcotic
Violations or 35B = Drug Equipment Violations and
Data Element 7 (Offense Attempted/Completed) is
C = Completed.

__{081}-5__ `PropertyOffenseLinkValidationRule.Validate`  
Data Element 14 (Type Property Loss/Etc.) must
be 2 = Burned when Data Element 6 (UCR Offense
Code) is 200 = Arson and Data Element 7 (Offense
Attempted/Completed) is C = Completed.

__{081}-6__ `PropertyOffenseLinkValidationRule.Validate`  
Data Element 14 (Type Property Loss/Etc.) must
be 3 = Counterfeited/Forged, 5 = Recovered, or 6 =
Seized when Data Element 6 (UCR Offense Code)
is 250 = Counterfeiting/Forgery and Data Element
7 (Offense Attempted/Completed) is C =
Completed.

__{081}-7__ `PropertyOffenseLinkValidationRule.Validate`  
Data Element 14 (Type Property Loss/Etc.) must
be 4 = Destroyed/Damaged/Vandalized when Data
Element 6 (UCR Offense Code) is 290 =
Destruction/Damage/Vandalism of Property and
Data Element 7 (Offense Attempted/Completed) is
C = Completed.

__{081}-8__ `PropertyOffenseLinkValidationRule.Validate`  
Data Element 14 (Type Property Loss/Etc.) must
be 5 = Recovered or 7 = Stolen/Etc. when Data
Element 6 (UCR Offense Code) is any of the
following and Data Element 7 (Offense
Attempted/Completed) is C = Completed:
- 120 = Robbery
- 210 = Extortion/Blackmail
- 23A = Pocket-picking
- 23B = Purse Snatching
- 23C = Shoplifting
- 23D = Theft from Building
- 23E = Theft from Coin-Operated Machine or Device
- 23F = Theft from Motor Vehicle
- 23G = Theft of Motor Vehicle Parts or Accessories
- 23H = All other Larceny
- 240 = Motor Vehicle Theft
- 26A = False Pretenses/Swindle/Confidence Game
- 26B = Credit Card/Automated Teller Machine Fraud
- 26C = Impersonation
- 26D = Welfare Fraud
- 26E = Wire Fraud
- 270 = Embezzlement
- 26F = Identity Theft
- 26G = Hacking/Computer Invasion
- 270 = Embezzlement

__{081}-9__ `PropertyOffenseLinkValidationRule.Validate`  
Data Element 14 (Type Property Loss/Etc.) must
be 6 = Seized when Data Element 6 (UCR Offense
Code) is any of the following and Data Element 7
(Offense Attempted/Completed) is C = Completed:
- 39A = Betting/Wagering
- 39B = Operating/Promoting/Assisting Gambling
- 39C = Gambling Equipment Violation
- 39D = Sports Tampering

__{084}__ `PropertyValueValidationRule.Validate`  
Data Element 16 (Value of Property) for property
classified as 7 = Stolen/Etc. in Data Element 14
(Type Property Loss/Etc.) must be greater than or
equal to the value entered in Data Element 16
(Value of Property) for property classified as 5 =
Recovered for the same property specified in Data
Element 15 (Property Description) in an incident.  
__Note__: This edit also applies when a vehicle was
stolen and the recovered property in Data Element
15 (Property Description) is 38 = Vehicle
Parts/Accessories. The value of recovered parts
cannot exceed the value of stolen vehicles.

__{085}__ `OffenderVictimRelationshipValidationRule.Validate`  
Segment Level 4 (Victim Segment) with a data
value in Data Element 24 (Victim Connected to
UCR Offense Code) of a Crime Against Person or
Robbery must contain at least two offender
sequence numbers in Data Element 34 (Offender
Number to be Related) when there are three or
more Segment Level 5 (Offender Segment)
records submitted for the incident, and Data
Element 25 (Type of Victim) is I = Individual or L =
Law Enforcement.

__{088}__ `IbrTransformer.Transform` - *Handled automatically*  
Segment Level 6 (Arrestee Segment) and Segment
Level 7 (Group B Arrest Report Segment) cannot
have the same data values entered in Data
Element 2 (Incident Number) and Data Element 41
(Arrest Transaction Number), respectively, for the
same ORI.

__{090}__ `ZeroRecordIbrFormatter`  
A Segment Level 0 was submitted that had an
invalid reporting month in positions 38 through 39.
The data entered must be a valid month of 01
through 12.

__{091}__ `ZeroRecordIbrFormatter`  
A Segment Level 0 was submitted that did not
have four numeric digits in positions 40 through 43.

__{092}__ `ZeroRecordIbrFormatter`  
(Incident Number) Zero-Reporting Segment (Level
0) must contain 12 zeros as the incident number.

__{093}__ `IbrSubmissionGenerator.Generate`  
A Segment Level 0 was submitted with a month
and year entered into positions 38 through 43 that
is earlier than January 1 of the previous year or
before the date the agency converted over to
NIBRS.

__{094}__ `IbrSubmissionGenerator.Generate`  
A Segment Level 0 was submitted with a month
and year entered into positions 38 through 43 that
was later than the Month of Electronic submission
and Year of Electronic submission entered into
positions 7 through 12

__{096}__ - *Let Fail*  
The combined Zero Report Month and Zero Report
Year cannot be on or after the date a law
enforcement agency (LEA) is placed in Covered-by
Status. When Zero Report data are received for a
LEA in Covered-by Status, the FBI will remove the
agency from Covered-by Status, process the
submission, and notify the Agency. Additionally,
adjustments to previously submitted data from an
agency now in Covered-by Status will be
processed and no error generated.

__{930}__ - *Let Fail*  
ORI is no longer able to submit data because they
have been marked for historical reference only

__{931}__ - *Let Fail*  
The ORI already exists in the reference table for a
different agency. Validate that the ORI is accurate.

__{932}__ - *Let Fail*  
The ORI submitted was not found in the reference
data table. Verification is needed to ensure that
the submitting agency is providing an accurate ORI
for the data submission.

__{933}__ - *Let Fail*  
The ORI submitted is currently in a covered by
status. Verification is needed to ensure that the
information being provided is accurate and or to
ensure that the submitting agency is now providing
its own information and is no longer in covered by
status.

__{934}__ - *Let Fail*  
The ORI submitted is in delete status. Verification
is needed to ensure this is accurate.

### Data Quality Checks

__{1342}__ `PropertyValueValidationRule.Validate`  
When referenced data element contains a value
that exceeds an FBI-assigned threshold amount, a
warning message will be created. The participant
is asked to check to see if the value entered was a
data entry error, or if it was intended to be entered.
A warning message is always produced when the
value is $1,000,000 or greater. For example, if the
value of a property is $12,000.99 but is
inadvertently entered as $1,200,099 in the
computer record sent to the FBI, a warning
message will be generated. In this case, the cents
were entered as whole dollars.

__{1343}__ `IbrTransformer.BuildPropertyRecord`  
A warning is generated when the incident contains
an offense segment where Data Element 6 (UCR
Offense Code) is 280 = Stolen Property Offense,
another offense segment where Data Element 6 is
240 = Motor Vehicle Theft, a property segment
where Data Element 14 (Type Property Loss/Etc.)
is 5 = Recovered and Data Element 15 (Property
Description) is a vehicle (see below for vehicle
property descriptions), but no property segment
exists where Data Element 14 is 7 = Stolen/Etc.
and Data Element 15 is a vehicle.
Data Element 15 (Property Description) for
vehicles:
- 03 = Automobiles
- 05 = Buses
- 24 = Other Motor Vehicles
- 28 = Recreational Vehicles
- 37 = Trucks
The incident should be reviewed and if there was
indeed a stolen vehicle, the incident should be
resubmitted reflecting both stolen and recovered
vehicles.

__{1449}__ `VictimAgeValidationRule.Validate`  
Data Element 26 (Age of Victim) cannot be 13, 14,
15, 16, or 17 when Data Element 35 (Relationship
of Victim to Offender) contains a relationship of SE
= Spouse.

__{1549}__ `OffenderVictimRelationshipValidationRule.Validate`  
Data Element 37 (Age of Offender) cannot be 13,
14, 15, 16, or 17 when Data Element 35
(Relationship of Victim to Offender) contains a
relationship of SE = Spouse.

__{1640}__ `OffenderAgeValidationRule.Validate`  
Data Element 52 (Disposition of Arrestee Under
18) was not entered, but Data Element 47 (Age of
Arrestee) indicates an age-range for a juvenile.
The low age is a juvenile and the high age is an
adult, but the average age is a juvenile.
Note: When an age-range is not entered and the
age is a juvenile, then the disposition must be
entered. These circumstances were flagged by the
computer as a possible discrepancy between age
and disposition and should be checked for
possible correction by the participant.

__{1641}__ `IbrAgeFormatter.FormatIbrArrestee`  
Data Element 47 (Age of Arrestee) was entered
with a value of 99 which means the arrestee was
over 98 years old. Verify that the submitter of data
is not confusing the 99 = Over 98 Years Old with
00 = Unknown.

__{1740}__ `OffenderAgeValidationRule.Validate`  
Data Element 52 (Disposition of Arrestee Under
18) was not entered, but Data Element 47 (Age of
Arrestee) indicates an age-range for a juvenile.
The low age is a juvenile and the high age is an
adult, but the average age is a juvenile.
Note: When an age-range is not entered and the
age is a juvenile, the disposition must be entered.
These circumstances were flagged by the
computer as a possible discrepancy between age
and disposition and should be checked for
possible correction by the participant.

__{1741}__ `IbrAgeFormatter.FormatIbrArrestee`  
Data Element 47 (Age of Arrestee) was entered
with a value of 99, which means the arrestee is
over 98 years old. The submitter should verify that
99 = Over 98 Years Old is not being confused with
00 = Unknown.

### Multi-Level Errors

__{001}__ __{101}__ __{201}__ __{301}__ __{401}__ __{501}__ __{601}__ __{701}__  
The referenced data element in an Incident must
contain data when the referenced data element is
mandatory or when the conditions are met for
data that must be entered into a conditionally
mandatory field.
- Segment Length - *Handled by Formatters*  
- Segment Level - *Handled by Formatters*
- Segment Action Type - *Handled by Formatters*
- Month of Submission - *Handled by Transformer*
- Year of Submission - *Handled by Transformer*
- ORI - *Handled by Formatters*  
__{001}__
- Zero Report Month - `IbrTransformer.BuildZeroReportSegment`
- Zero Report Year - `IbrTransformer.BuildZeroReportSegment`  
__{101}__
- Incident Number - `IncidentNumberValidator.Validate`
- Incident Date - `AdminOccurredFromValidationRule.Validate`
- Cleared Exceptionally - `AdminRecordIbrFormatter`  
__{201}__
- UCR Offense Code - `OffenseUcrCodeValidationRule.Validate`
- Offense Attempted/Completed - `OffenseAttemptedCompletedValidationRule.Validate`
- Bias Motivation - `OffenseBiasValidationRule.Validate`
- Location Type - `OffenseLocationValidationRule.Validate`  
__{301}__
- Suspected Drug Type - `PropertyDrugTypeValidationRule.Validate`  
__{401}__
- Victim Sequence Number - `VictimSequenceNumberValidationRule.Validate`
- Victim Connected to UCR Offense Code - `VictimOffenseLinkValidationRule.Validate`
- Type of Victim - `VictimTypeValidationRule.Validate`
- Type of Injury - `VictimInjuryValidationRule.Validate`
- Offender Number to be Related `OffenderVictimRelationshipValidationRule.Validate`  
__{501}__
- Offender Sequence Number - `OffenderSequenceNumberValidationRule.Validate`
- Age of Offender - `IbrAgeFormatter.FormatIbrOffender`
- Sex of Offender - `OffenderGenderValidationRule.Validate`
- Race of Offender - `OffenderRaceValidationRule.Validate`  
__{601}__
- Multiple Arrestee Segments Indicator - `OffenderMultipleArrestValidationRule.Validate`  
__{601}__ __{701}__
- Arrestee Sequence Number - *This would follow Offender Sequence Number automatically*
- Arrest Transaction Number - `OffenderTcnNumberValidationRule.Validate`
- Arrest Date - `OffenderArrestDateTimeValidationRule.Validate`
- Type of Arrest - `OffenderTypeValidationRule.Validate`
- UCR Arrest Offense Code - `IbrTransformer.BuildGroupAArresteeRecord`, `IbrTransformer.BuildGroupBArresteeRecord`
- Arrestee Was Armed With - `OffenderArmedWithValidationRule.Validate`
- Age of Arrestee - `IbrAgeFormatter.FormatIbrArrestee`
- Sex of Arrestee - `OffenderGenderValidationRule.Validate`
- Race of Arrestee - `OffenderRaceValidationRule.Validate`

__{104}__ __{204}__ __{304}__ __{404}__ __{504}__ __{604}__ __{704}__  
The referenced data element must contain a valid
data value when it is entered; blank is permissible
on non-mandatory fields.
- City Indicator - *Handled by Formatters*  
__{104}__
- Cargo Theft - `AdminRecordIbrFormatter`
- Report Date Indicator - `AdminRecordIbrFormatter`
- Incident Hour - `AdminRecordIbrFormatter`
- Cleared Exceptionally - `AdminRecordIbrFormatter`
- Exceptional Clearance Date - `AdminRecordIbrFormatter`  
__{204}__
- UCR Offense Code - `OffenseRecordIbrFormatter`
- Offender Suspected of Using - `OffenseRecordIbrFormatter`
- Bias Motivation - `OffenseRecordIbrFormatter`
- Location Type - `OffenseRecordIbrFormatter`
- Method of Entry - `OffenseRecordIbrFormatter`
- Type Criminal Activity/Gang Information - `OffenseRecordIbrFormatter`
- Type Weapon/Force Involved - `OffenseRecordIbrFormatter`
- Automatic Weapon Indicator - `OffenseRecordIbrFormatter`  
__{304}__
- Type Property Loss/Etc. - `PropertyRecordIbrFormatter`
- Property Description - `PropertyRecordIbrFormatter`
- Suspected Drug Type - `PropertyRecordIbrFormatter`
- Type Drug Measurement - `PropertyRecordIbrFormatter`  
__{404}__
- Victim Sequence Number - `VictimRecordIbrFormatter`
- Victim Connected to UCR Offense Code - `VictimRecordIbrFormatter`
- Type of Victim - `VictimRecordIbrFormatter`
- Type of Officer Activity/Circumstance - `VictimRecordIbrFormatter`
- Officer Assignment Type - `VictimRecordIbrFormatter`
- Age of Victim - `VictimRecordIbrFormatter`
- Sex of Victim - `VictimRecordIbrFormatter`
- Race of Victim - `VictimRecordIbrFormatter`
- Ethnicity of Victim - `VictimRecordIbrFormatter`
- Resident Status of Victim - `VictimRecordIbrFormatter`
- Aggravated Assault/Homicide Circumstances - `VictimRecordIbrFormatter`
- Additional Justifiable Homicide Circumstances - `VictimRecordIbrFormatter`
- Type of Injury - `VictimRecordIbrFormatter`
- Relationship of Victim to Offender - `VictimRecordIbrFormatter`  
__{504}__
- Sex of Offender - `OffenderRecordIbrFormatter`
- Race of Offender - `OffenderRecordIbrFormatter`
- Ethnicity of Offender - `OffenderRecordIbrFormatter`  
__{604}__
- Multiple Arrestee Segments Indicator - `GroupAArresteeIbrFormatter`   
__{604}__ __{704}__
- Type of Arrest - `GroupAArresteeIbrFormatter`, `GroupBArresteeIbrFormatter`
- UCR Arrest Offense Code - `GroupAArresteeIbrFormatter`, `GroupBArresteeIbrFormatter`
- Arrestee Was Armed With - `GroupAArresteeIbrFormatter`, `GroupBArresteeIbrFormatter`
- Automatic Weapon Indicator - `GroupAArresteeIbrFormatter`, `GroupBArresteeIbrFormatter`
- Race of Arrestee - `GroupAArresteeIbrFormatter`, `GroupBArresteeIbrFormatter`
- Ethnicity of Arrestee - `GroupAArresteeIbrFormatter`, `GroupBArresteeIbrFormatter`
- Resident Status of Arrestee - `GroupAArresteeIbrFormatter`, `GroupBArresteeIbrFormatter`
- Disposition of Arrestee Under 18 - `GroupAArresteeIbrFormatter`, `GroupBArresteeIbrFormatter`

__{115}__ __{215}__ __{315}__ __{415}__ __{515}__ __{615}__ `IbrFieldFormatter.PadText`, *embedded blanks prevented by form*  
(Incident Number) Must be blank right-fill if under 12
characters in length. Cannot have embedded blanks between
the first and last characters entered.

__{116}__ __{216}__ __{316}__ __{416}__ __{516}__ __{616}__ `IbrFieldFormatter.PadText`  
(Incident Number) must be left-justified with blank
right-fill. Since the number is less than 12
characters, it must begin in position 1.

__{117}__ __{217}__ __{317}__ __{417}__ __{517}__ __{617}__ `IncidentNumberValidator.Validate`  
(Incident Number) can only have character
combinations of A through Z, 0 through 9,
hyphens, and/or blanks. For example, 89-123-SC
is valid, but 89+123*SC is invalid.

### Zero Report Segment Errors

__{028}__ - *Let Fail*  
An agency cannot submit a zero report segment
when a Group A Incident is already on file for the
month/year.

__{029}__ - *Let Fail*  
An agency cannot submit a zero report segment
when a Group B Arrest Report is already on file for
the month/year.

### Administrative Segment Errors

__{105}__ `AdminReportDateTimeValidationRule.Validate`, `AdminExceptionalClearanceDateTimeValidationRule.Validate`  
The data element in error contains a date that is
not entered correctly. Each component of the date
must be valid; that is, months must be 01 through
12, days must be 01 through 31, and year must
include the century (i.e., 19xx, 20xx). In addition,
days cannot exceed maximum for the month (e.g.,
June cannot have 31days). Also, the date cannot
exceed the current date.

__{106}__ `AdminOccurredFromValidationRule.Validate`  
(Incident Hour) For Offenses of 09A, 13A, 13B
and 13C ONLY–When data element 25 (Type of
Victim) = L (Law Enforcement Officer) then Data
Element 3 (Incident Date/Hour) must be populated
with a valid hour (00-23). Incident Hour Unknown
(Blank) is not a valid entry.

__{118}__ - *Allow to Fail*  
The UCR Program has determined that an ORI
will no longer be submitting data to the FBI as of
an inactive date. No data from this ORI will be
accepted after this date.

__{119}__ `IbrTransformer.BuildAdminRecord`  
Data Element 2A (Cargo Theft) must be blank,
unless Data Element 6 (UCR Offense Code)
includes at least one of the following:
- 120 = Robbery
- 210 = Extortion/Blackmail
- 220 = Burglary/Breaking & Entering
- 23D = Theft From Building
- 23F = Theft From Motor Vehicle
- 23H = All Other Larceny
- 240 = Motor Vehicle Theft
- 26A = False Pretenses/Swindle/Confidence Game
- 26B = Credit Card/Automated Teller Machine Fraud
- 26C = Impersonation
- 26E = Wire Fraud
- 270 = Embezzlement
- 510 = Bribery

__{122}__ `IbrTransformer.BuildAdminRecord`  
Data Element 2A (Cargo Theft) must be populated
with a Y = Yes or N = No when Data Element 6
(UCR Offense Code) includes at least one of the
following:
- 120 = Robbery
- 210 = Extortion/Blackmail
- 220 = Burglary/Breaking & Entering
- 23D = Theft From Building
- 23F = Theft From Motor Vehicle
- 23H = All Other Larceny
- 240 = Motor Vehicle Theft
- 26A = False Pretenses/Swindle/Confidence Game
- 26B = Credit Card/Automated Teller Machine Fraud
- 26C = Impersonation
- 26E = Wire Fraud
- 26F = Identity Theft
- 26G = Hacking/Computer Invasion
- 270 = Embezzlement
- 510 = Bribery

__{151}__ `IbrTransformer.BuildAdminRecord`  
This field must be blank if the incident date is
known. If the incident date is unknown, then the
report date would be entered instead and must be
indicated with an “R” in the Report Indicator field
within the Administrative Segment.

__{152}__ - *Handled by the DateTime format*  
If Hour is entered within Data Element 3 (Incident
Date/Hour), it must be 00 through 23. If 00 =
Midnight is entered, be careful that the Incident
Date is entered as if the time was 1 minute past
midnight.  
__Note__: When an incident occurs exactly at
midnight, Data Element 3 (Incident Date) would
be entered as if the time is 1 minute past
midnight. For example, when a crime occurred
exactly at midnight on Thursday, Friday’s date
would be entered.

__{153}__ `AdminExceptionalClearanceValidationRule.Validate`  
Data Element 4 (Cleared Exceptionally) cannot be
N = Not Applicable if Data Element 5 (Exceptional
Clearance Date) is entered.

__{155}__ `AdminExceptionalClearanceValidationRule.Validate`  
Data Element 5 (Exceptional Clearance Date) is
earlier than Data Element 3 (Incident Date/Hour).

__{156}__ `AdminExceptionalClearanceValidationRule.Validate`  
Data Element 5 (Exceptional Clearance Date)
must be present if the case was cleared
exceptionally. Data Element 4 (Cleared
Exceptionally) has an entry of A through E;
therefore, the date must also be entered.

__{170}__ `AdminOccurredFromValidationRule.Validate`  
Data Element 3 The date cannot be later than the
year and month the electronic submission
represents. For example, the May 1999 electronic
submission cannot contain incidents happening
after this date.

__{172}__ `AdminReportDateTimeValidationRule.Validate`, `AdminOccurredFromValidationRule.Validate`  
Data Element 3 (Incident Date) cannot be earlier
than 01/01/1991. This edit will preclude dates that
are obviously incorrect since the FBI began
accepting NIBRS data on this date.

__{173}__ - *Let Fail*  
A Group “A” Incident Report was submitted with
Data Element 3 (Incident Date/Hour) containing a
date that occurred before the agency converted
over to NIBRS. Because of this, the record was
rejected. At some point, the participant will convert
its local agencies from Summary reporting to
Incident-Based Reporting. Once the participant
starts to send NIBRS data for a converted agency
to the FBI, any data received from this agency
that could possibly be interpreted as duplicate
reporting within both the Summary System and
NIBRS for the same month will be rejected by the
FBI. In other words, if the participant sends IBR
data for an agency for the first time on September
1999, monthly submittal, dates for incidents,
recovered property, and arrests must be within
September. The exception is when exceptional
clearances occur for a pre-IBR incident. In this
case, Data Element 3 (Incident Date/Hour) may
be earlier than September 1999, but Data
Element 5 (Exceptional Clearance Date) must be
within September 1999. The FBI will reject data
submitted for prior months. Thereafter, all data
coming from this agency must have dates
subsequent to the initial start date of September
1999, except as mentioned previously. The
Summary System already contains aggregate
data for the months prior to NIBRS conversion. If
the FBI were to accept IBR data for months
previously reported with Summary data, the result
would be duplicate reporting for those months.

__{175}__ `IbrTransformer.BuildAdminRecord`  
The electronic submission control date (positions
7 through 12, month and year) and Data Element
3 (Incident Date/Hour) must both be valid dates
for calculating timeframes.

__{178}__ `AdminRecordIbrFormatter.FormatAdminRecord`  
Segment Length for the Administrative Segment
(Level 1) must be 87 characters (not reporting
Cargo Theft) or 88 characters (reporting Cargo
Theft). All Administrative Segments in a
submission must be formatted in only one of these
two lengths

__{197}__ `IbrTransformer.BuildDeleteRecord`  
Data Element 3 (Incident Date) is missing for a
Group A Incident Report with a Segment Action
Type of D = Delete; must be populated with a
valid data value and cannot be blank.

__{198}__ `IbrTransformer.BuildDeleteRecord`  
Data Element 3 (Incident Date) is missing for a
Group A Incident Report with a Segment Action
Type of D = Delete; at least one Group A Incident
Report is on file that matches Data Element 2
(Incident Number) with an Incident Date outside
the two year window.

__{199}__ `IbrTransformer.BuildDeleteRecord`  
Data Element 3 (Incident Date) is missing for a
Group A Incident Report with a Segment Action
Type of D = Delete; must be populated with a
valid data value and cannot be blank.

### Offense Segment Errors

__{202}__ `OffensePremisesValidationRule.Validate`  
Data Element 10 (Number of Premises Entered) is
not a numeric entry of 01 through 99.

__{205}__ `OffenseLocationValidationRule.Validate`  
Data Element 9 (Location Type) = Cyberspace,
can only be entered when Data Element 6
Offense Code is one of the violations listed below:
- 210 = Extortion/Blackmail
- 250 = Counterfeiting/Forgery
- 270 = Embezzlement
- 280 = Stolen Property Offenses
- 290 = Destruction/Damage/Vandalism of Property
- 370 = Pornography/Obscene Material
- 510 = Bribery
- 26A = False Pretenses/Swindle/Confidence Game
- 26B = Credit Card/Automated Teller Machine Fraud
- 26C = Impersonation
- 26D = Welfare Fraud
- 26E = Wire Fraud
- 26F = Identity Theft
- 26G = Hacking/Computer Invasion
- 39A = Betting/Wagering
- 39B = Operating/Promoting/Assisting Gambling
- 39C = Gambling Equipment Violations
- 13C = Intimidation
- 35A = Drug/Narcotic Violations
- 35B = Drug Equipment Violations
- 520 = Weapon Law Violations
- 64A = Human Trafficking, Commercial Sex Acts
- 64B = Human Trafficking, Involuntary Servitude
- 40A = Prostitution
- 40B = Assisting or Promoting Prostitution
- 40C = Purchasing Prostitution

__{206}__ `OffenseUsingValidationRule.Validate`, `OffenseBiasValidationRule.Validate`, `OffenseActivityValidationRule.Validate`, `OffenseWeaponValidationRule.Validate`  
The referenced data element in error is one that
contains multiple data values. When more than
one code is entered, none can be duplicate
codes.

__{207}__ `OffenseUsingValidationRule.Validate`, `OffenseBiasValidationRule.Validate`, `OffenseActivityValidationRule.Validate`, `OffenseWeaponValidationRule.Validate`  
The data element in error can have multiple data
values and was entered with multiple values.
However, mutually exclusive values cannot be
entered with any other data value. Refer to
individual data elements for mutually exclusive
data values.

__{219}-1__ `OffenseActivityValidationRule.Validate`, `IbrTransformer.GetOffenseActivityValue`  
Data Element 12 (Type Criminal Activity/Gang
Information) Type criminal activity codes of “B”,
“C”, “D”, “E”, “O”, “P”, “T”, or “U” can only be
entered when the UCR Offense Code is:
- 250 = Counterfeiting/Forgery
- 280 = Stolen Property Offenses
- 35A = Drug/Narcotic Violations
- 35B = Drug Equipment Violations
- 39C = Gambling Equipment Violations
- 370 = Pornography/Obscene Material
- 520 = Weapon Law Violations

__{219}-2__ `OffenseActivityValidationRule.Validate`, `IbrTransformer.GetOffenseActivityValue`  
(Type Criminal Activity/Gang Information) Gang
information codes of “J”, “G”, and “N” can only be
entered when the UCR Offense Code is:
- 09A = Murder and Non-negligent Manslaughter
- 09B = Negligent Manslaughter
- 100 = Kidnapping/Abduction
- 11A = Rape
- 11B = Sodomy
- 11C = Sexual Assault With An Object
- 11D = Fondling
- 120 = Robbery
- 13A = Aggravated Assault
- 13B = Simple Assault
- 13C = Intimidation

__{219}-3__ `OffenseActivityValidationRule.Validate`, `IbrTransformer.GetOffenseActivityValue`  
(Type Criminal Activity/Gang Information) Criminal
Activity codes of “A”, “F”, “I”, and “S” can only be
entered when the UCR Offense Code is:
- 720 = Animal Cruelty

__{220}__ `OffenseActivityValidationRule.Validate`  
Data Element 12 (Type Criminal Activity/Gang
Information) Must be populated with a valid data
value and cannot be blank when Data Element 6
(UCR Offense Code) is:
- 250 = Counterfeiting/Forgery
- 280 = Stolen Property Offenses
- 35A = Drug/Narcotic Violations
- 35B = Drug Equipment Violations
- 39C = Gambling Equipment Violations
- 370 = Pornography/Obscene Material
- 520 = Weapon Law Violations
- 720 = Animal Cruelty

__{221}__ `OffenseWeaponValidationRule.Validate`  
Data Element 13 (Type Weapon/Force Involved)
must be populated with a valid data value and
cannot be blank when Data Element 6 (UCR
Offense Code) is:
- 09A = Murder and Non-negligent Manslaughter
- 09B = Negligent Manslaughter
- 09C = Justifiable Homicide
- 100 = Kidnapping/Abduction
- 11A = Rape
- 11B = Sodomy
- 11C = Sexual Assault With An Object
- 11D = Fondling
- 120 = Robbery
- 13A = Aggravated Assault
- 13B = Simple Assault
- 210 = Extortion/Blackmail
- 520 = Weapon Law Violations
- 64A = Human Trafficking, Commercial Sex Acts
- 64B = Human Trafficking, Involuntary Servitude

__{222}__ `OffenseWeaponValidationRule.Validate`, `IbrTransformer.BuildOffenseRecord`  
Data Element 13 (Type of Weapon/Force
Involved) Type of Weapon/Force Involved can
only be entered when the UCR Offense Code is:
- 09A = Murder and Non-negligent Manslaughter
- 09B = Negligent Manslaughter
- 09C = Justifiable Homicide
- 100 = Kidnapping/Abduction
- 11A = Rape
- 11B = Sodomy
- 11C = Sexual Assault With An Object
- 11D = Fondling
- 120 = Robbery
- 13A = Aggravated Assault
- 13B = Simple Assault
- 210 = Extortion/Blackmail
- 520 = Weapon Law Violations
- 64A = Human Trafficking, Commercial Sex Acts
- 64B = Human Trafficking, Involuntary Servitude

__{251}__ `OffenseAttemptedCompletedValidationRule.Validate`  
(Offense Attempted/Completed) Must be a valid
code of A = Attempted or C = Completed.

__{252}__ `OffensePremiseValidationRule.Validate`, `IbrTransformer.GetOffensePremisesValue`  
When Data Element 10 (Number of Premises
Entered) is entered, Data Element 9 (Location
Type) must be 14 = Hotel/Motel/Etc. or 19 =
Rental Storage Facility, and Data Element 6 (UCR
Offense Code) must be 220 (Burglary).

__{253}__ `OffenseEntryMethodValidationRule.Validate`  
Data Element was not entered; it must be entered
when UCR Offense Code of 220 = Burglary has
been entered.

__{254}__ `OffenseEntryMethodValidationRule.Validate`  
Data Element only applies to UCR Offense Code
of 220 = Burglary. Since a burglary offense was
not entered, the Method of Entry should not have
been entered.

__{255}__ `IbrTransformer.GetIbrAutomaticFirearmCode`  
Must be A = Automatic or blank = Not Automatic

__{256}__ `OffenseAttemptedCompletedValidationRule.Validate`  
Code must be C = Completed if Data Element 6
(UCR Offense Code) is an Assault or Homicide.

__{257}__ `OffensePremisesValidationRule.Validate`  
Must be entered if offense code is 220 (Burglary)
and if Data Element 9 (Location Type) contains 14
= Hotel/Motel/Etc. or 19 = Rental Storage Facility.

__{258}__ - *Handled by Weapon Type Codes*  
In Data Element 13 (Type of Weapon/Force
Involved), A = Automatic is the third character of
code. It is valid only with the following codes:
- 11 = Firearm (Type Not Stated)
- 12 = Handgun
- 13 = Rifle
- 14 = Shotgun
- 15 = Other Firearm  
A weapon code other than those mentioned was
entered with the automatic indicator. An automatic
weapon is, by definition, a firearm.

__{262}__ `OffenseUcrCodeValidationRule.Validate`  
When a Group “A” Incident Report is submitted,
the individual segments comprising the incident
cannot contain duplicates. In this case, two
Offense Segments were submitted having the
same offense in Data Element 6 (UCR Offense
Code).

__{263}__ `OffenseCountValidationRule.Validate`  
Can be submitted only 10 times for each Group A
Incident Report; 10 offense codes are allowed for
each incident.

__{264}__ `IbrPreprocessor.PreprocessIncident`  
Data Element 6 (UCR Offense Code) must be a
Group “A” UCR Offense Code, not a Group “B”
Offense Code.

__{265}__ `OffenseWeaponValidationRule.Validate`  
If an Offense Segment (Level 2) was submitted for
13B = Simple Assault, Data Element 13 (Type
Weapon/Force Involved) can only have codes of
40 = Personal Weapons, 90 = Other, 95 =
Unknown, and 99 = None. All other codes are not
valid because they do not relate to a simple
assault.

__{266}__ `OffenseUcrCodeValidationRule.ValidateJustifiedHomicideOffense`  
When a Justifiable Homicide is reported, no other
offense may be reported in the Group “A” Incident
Report. These should be submitted on another
Group “A” Incident Report.

__{267}__ `OffenseWeaponValidationRule.Validate`  
If a homicide offense is submitted, Data Element
13 (Type Weapon/Force Involved) cannot have 99
= None. Some type of weapon/force must be used
in a homicide offense.

__{268}__ `PropertyOffenseLinkValidationRule.Validate`  
Data Element 6 (UCR Offense Code) is a (23A-
23H) = Larceny/Theft Offenses and a property
segment exists with Data Element 14 (Type
Property Loss/Etc.) of 7 = Stolen/Etc. and Data
Element 15 (Property Description) of 03 =
Automobiles, 05 = Buses, 24 = Other Motor
Vehicles, 28 = Recreational Vehicles, or 37 =
Trucks; then the incident must contain another
offense segment with Data Element 6 (UCR
Offense Code) containing a crime against
property other than a larceny/theft offense (refer
to Appendix Mandatories for each crime against
property offense).

__{269}__ `OffenseWeaponValidationRule.Validate`  
If Data Element 6 (UCR Offense Code) is 13B =
Simple Assault and the weapon involved is 11 =
Firearm, 12 = Handgun, 13 = Rifle, 14 = Shotgun,
or 15 = Other Firearm, then the offense should
instead be classified as 13A = Aggravated
Assault.

__{270}__ `OffenseBiasMotivationRule.Validate`  
Must be 88 = None when Data Element 6 (UCR
Offense Code) is 09C = Justifiable Homicide.

__{284}__ `OffenseRecordIbrFormatter.FormatOffenseRecord`
Segment Length for the Offense Segment (Level
2) must be 63 characters (reporting only Bias
Motivation #1) or 71 characters (reporting Bias
Motivations #2–#5). All Offense Segments in a
submission must be formatted in only one of these
two lengths.  
*NOTE: This length is incorrect due to the VA-specific addendum*

__{302}__ `IbrTransformer.BuildPropertyRecordDto`  
Must be numeric entry with zero left-fill. Refer to
individual data element for specific formatting
instructions.

__{305}__ `PropertyRecoveredDateTimeValidationRule.Validate`  
Each component of the date must be valid; that is,
months must be 01 through 12, days must be 01
through 31, and year must include the century (i.e.,
19xx, 20xx). In addition, days cannot exceed
maximum for the month (e.g., June cannot have 31
days). The date cannot be later than that entered
within the Month of Electronic Submission and
Year of Electronic submission fields on the data
record. For example, if Month of Electronic
Submission and Year of Electronic Submission are
06/1999, the recovered date cannot contain any
date 07/01/1999 or later. Cannot be earlier than
Data Element 3 (Incident Date/Hour).

__{306}__ `IbrTransformer.BuildPropertyRecordDto`  
The referenced data element in error is one that
contains multiple data values. When more than one
code is entered, none can be duplicate codes.
Refer to individual data elements for exceptions.

__{351}__ `IbrTransformer.BuildPropertyRecordDto`  
Data Element 16 (Value of Property) Cannot be
zero unless Data Element 15 (Property
Description) is:  
Mandatory zero
- 09=Credit/Debit Cards
- 22=Nonnegotiable Instruments
- 48=Documents–Personal or Business
- 65=Identity Documents
- 66=Identity–Intangible

Optional zero
- 77=Other
- 99=(blank)–this data value is not currently
used by the FBI.

__{352}__ `PropertyTypeValidationRule.Validate`, `PropertyValueValidationRule.Validate`, `PropertyRecoveredDateTimeValidationRule.Validate`, `IbrTransformer.BuildPropertyRecordDto`, `PropertyDrugTypeValdationRule.Validate`, `PropertyDrugQuantityValidationRule.Validate`, `PropertyDrugUnitOfMeasurementValidationRule.Validate`  
Referenced data elements must be blank when one
of the following criteria is met:
1) If Data Element 14 (Type property Loss/Etc.) is
8=Unknown, then Data Elements 15 through
22 must be blank.
2) If Data Element 14 (Type property Loss/Etc.) is
1=None and Data Element 6 (UCR Offense
Code) is 35A=Drug/ Narcotic Violations, then
Data Elements 15 through 19 and 21 through
22 must be blank, and Data Element 20
(Suspected Drug Type) must be entered,
unless the incident contains another crime that
requires a property segment..
3) If Data Element 14 (Type property Loss/Etc.) is
1=None and the incident has no offense
segments with a Data Element 6 (UCR
Offense Code) of 35A=Drug/ Narcotic
Violations, then Data Elements 15 through 22
must be blank.

__{353}__ `IbrTransformer.BuildPropertyRecordDto`  
Data Element 15 (Property Description) is
88=Pending Inventory, but Data Element 16 (Value
of Property) is not $1.

__{354}__ `PropertyTypeValidationRule.Validate`  
Data Element 16 (Value of Property) contains a
value, but Data Element 15 (Property Description)
was not entered.

__{355}__ `PropertyRecoveredDateTimeValidationRule.Validate`  
Data Element 14 (Type Property Loss/Etc.) must
be 5=Recovered for Data Element 17 (Date
Recovered) to be entered.

__{356}__ `PropertyRecoveredDateTimeValidationRule.Validate`   
Referenced Data Element was entered, but Data
Elements 15 (Property Description) and/or 16
(Value of Property) were not entered.

__{357}__ `IbrTransformer.BuildPropertyRecordDto`  
Data Element 18 (Number of Stolen Motor
Vehicles) was entered. However, Data Element 14
(Type Property Loss/Etc.) 7=Stolen/Etc. was not
entered, and/or Data Element 6 (UCR Offense
Code) of 240=Motor Vehicle Theft was not
entered, and/or Data Element 7 (Offense
Attempted/Completed) was A=Attempted.

__{358}__ `IbrTransformer.BuildPropertyRecordDto`  
Entry must be made for Data Element 18 (Number
of Stolen Motor Vehicles) when Data Element 6
(UCR Offense Code) is 240=Motor Vehicle Theft,
Data Element 7 (Offense Attempted/Completed) is
C=Completed, and Data Element 14 (Type
Property Loss/Etc.) is 7=Stolen/Etc.

__{359}__ `IbrPreprocessor.PreprocessVehicularInformation`  
Data Element 15 (Property Description) Must be
one of the following:
- 03=Automobiles
- 05=Buses
- 24=Other Motor Vehicles
- 28=Recreational Vehicles
- 37= Trucks

When Data Element 6 (UCR Offense Code) is 240
= Motor Vehicle Theft, and Data Element 18
(Number of Stolen Motor Vehicles) or Data
Element 19 (Number of Recovered Motor Vehicles)
contains a data value of 01-99.

__{360}__ `IbrTransformer.BuildPropertyRecordDto`  
Data Element 19 (Number of Recovered Motor
Vehicles) was entered. However, Data Element 14
(Type Property Loss/Etc.) 5=Recovered was not
entered, and/or Data Element 6 (UCR Offense
Code) of 240=Motor Vehicle Theft was not
entered, and/or Data Element 7 (Offense
Attempted/Completed) was A=Attempted.
The exception to this rule is when recovered
property is reported for a pre-NIBRS incident. In
this case, Segment Level 3 (Property Segment) will
contain A=Add, but the data value in Data Element
2 (Incident Number) will not match an incident
already on file in the national UCR database. The
segment will be processed, but used only for SRS
purposes and will not be included in the agency’s
NIBRS figures.

__{361}__ `IbrTransformer.BuildPropertyRecordDto`  
When Data Element 6 (UCR Offense Code) is 240
= Motor Vehicle Theft, Data Element 7 (Offense
Attempted/Completed) is C = Completed, Data
Element 14 (Type Property Loss/Etc.) is 5 =
Recovered, and vehicle codes were recovered.

__{362}__ `IbrTransformer.BuildPropertyRecordDto`   
Since X=Over 3 Drug Types was entered in Data
Element 20 (Suspected Drug Type), two other
codes must also be entered. There are less than
three codes present.

__{363}__ `IbrTransformer.BuildPropertyRecordDto`   
Since Data Element 20 (Suspected Drug Type)
contains X=Over 3 Drug Types, Data Element 21
(Estimated Quantity) and 22 (Type Measurement)
must be blank

__{364}__ `PropertyDrugQuantityValidationRule.Validate`, `PropertyDrugUnitOfMeasurementValidationRule.Validate`  
When Data Element 6 (UCR Offense Code) is
35A=Drug/Narcotic Violations, Data Element 14
(Type Property Loss/Etc.) is 6=Seized, Data
Element 15 (Property Description) is 10=Drugs,
and Data Element 20 (Suspected Drug Type) is
entered, both Data Element 21 (Estimated Drug
Quantity) and Data Element 22 (Type Drug
Measurement) must also be entered, unless Data
Element 20 (Suspected Drug Type) contains
X=Over 3 Drug Types.

__{365}__ `IbrTransformer.BuildPropertyRecordDto`, `PropertyDrugTypeValidationRule.Validate`  
Data Element 20 (Suspected Drug Type) was
entered, but one or more required data elements
were not entered. Data Element 6 (UCR Offense
Code) must be 35A=Drug/Narcotic Violations, Data
Element 14 (Type Property Loss/Etc.) must be
6=Seized, or 1 = None and if seized, Data
Element 15 (Property Description) must be
10=Drugs/Narcotics.

__{366}__ `PropertyDrugUnitOfMeasurementValidationRule.Validate`, `PropertyDrugTypeValidationRule.Validate`  
Data Element 21 (Estimated Quantity) was
entered, but 20 (Suspected Drug Type) and/or 22
(Type Measurement) were not entered; both must
be entered.

__{367}__ `PropertyDrugUnitOfMeasurementValidationRule.Validate`  
Data Element 22 (Type Measurement) was
entered with NP in combination with an illogical
drug type. Based upon the various ways a drug
can be measured, very few edits can be done to
check for illogical combinations of drug type and
measurement. The only restriction will be to limit
NP=Number of Plants to the following drugs:
DRUG MEASUREMENT
E=Marijuana NP
G=Opium NP
K=Other Hallucinogens NP
All other Data Element 22 (Type Measurement)
codes are applicable to any Data Element 20
(Suspected Drug Type) code.

__{368}__ `PropertyDrugUnitOfMeasurementValidationRule.Validate`, `PropertyDrugTypeValidationRule.Validate`  
Data Element 22 (Type Measurement) was
entered, but 20 (Suspected Drug Type) and/or 21
(Estimated Quantity) were not entered; both must
be entered.

__{372}__ `IbrTransformer.BuildPropertyRecordDto`, `PropertyTypeValidationRule.Validate`  
If Data Element 14 (Type Property/Loss/Etc.) is
2=Burned, 3=Counterfeited/ Forged,
4=Destroyed/Damaged/Vandalized, 5=Recovered,
6=Seized, or 7=Stolen/Etc., Data Elements 15
through 22 must have applicable entries in the
segment.

__{375}__ `PropertyTypeValidationRule.Validate`  
At least one Data Element 15 (Property
Description) code must be entered when Data
Element 14 (Type Property Loss/Etc.) contains
Property Segment(s) for:
- 2=Burned
- 3=Counterfeited/Forged
- 4=Destroyed/Damaged/Vandalized
- 5=Recovered
- 6=Seized
- 7=Stolen/Etc.

__{376}__ `IbrTransformer.BuildPropertyRecordDto`  
When a Group “A” Incident Report is submitted,
the individual segments comprising the incident
cannot contain duplicates. Example, two property
segments cannot be submitted having the same
entry in Data Element 14 (Type Property
Loss/Etc.).

__{382}__ `PropertyTypeValidationRule.Validate`  
Segment Level 3 (Property Segment) cannot be
submitted with 10=Drugs/Narcotics in Data
Element 15 (Property Description) and blanks in
Data Element 16 (Value of Property) unless Data
Element 6 (UCR Offense Code) is
35A=Drug/Narcotic Violations.

__{383}__ `IbrTransformer.BuildPropertyRecordDto`  
Data Element 16 (Value of Property) must be blank
when Data Element 15 (Property Description) code
is 10=Drugs/Narcotics and the only offense
submitted is a 35A=Drug/Narcotic Violations.

__{384}__ `IbrTransformer.BuildPropertyRecordDto`  
Data Element 21 (Estimated Drug Quantity) must
be 000000001000=None (i.e., 1) when Data
Element 22 (Type Drug Measurement) is XX=Not
Reported indicating the drugs were sent to a
laboratory for analysis.
When the drug analysis is received by the LEA,
Data Element 21 and Data Element 22 should be
updated with the correct data values.

__{387}__ `PropertyLossTypeValidationRule.Validate`  
1) 35A Drug/Narcotic Violation Offense without a
35B Drug Equipment Violation Offense and a
Property Type of 6 = Seized and Property
Description of 11 and no other crimes requiring a
property segment exist.
2) 35B Drug Equipment Violation Offense
without 35A Drug/Narcotic Violation Offense and a
Property Type of 6 = Seized and Property
Description of 10 and no other crimes requiring a
property segment exist.

__{388}__ `IbrPreprocessor.PreprocessVehicularInformation`  
More than one vehicle code was entered in Data
Element 15 (Property Description), but the number
stolen in Data Element 18 (Number of Stolen Motor
Vehicles) is less than this number. For example, if
vehicle codes of 03=Automobiles and 05=Buses
were entered as being stolen, then the number
stolen must be at least 2, unless the number stolen
was unknown (00).
The exception to this rule is when 00=Unknown is
entered in Data Element 18.

__{389}__ `IbrPreprocessor.PreprocessVehicularInformation`  
More than one vehicle code was entered in Data
Element 15 (Property Description), but the number
recovered in Data Element 19 (Number of
Recovered Motor Vehicles) was less than this
number. For example, if vehicle codes of
03=Automobiles and 05=Buses were entered as
being recovered, then the number recovered must
be at least 2, unless the number recovered was
unknown (00).
The exception to this rule is when 00=Unknown is
entered in Data Element 19.

__{390}__ `PropertyOffenseLinkValidationRule.Validate`  
(Property Description) must contain a data value
that is logical for one or more of the Crime Against
Property offenses entered in Data Element 6 (UCR
Offense Code).
Illogical combinations include:
7) Property descriptions for structures are illogical
with 220=Burglary/Breaking & Entering or
240=Motor Vehicle Theft
8) Property descriptions for items that would not fit
in a purse or pocket (aircraft, vehicles,
structures, a person’s identity, watercraft, etc.)
are illogical with 23A=Pocket-picking or
23B=Purse-snatching
9) Property descriptions that cannot be shoplifted
due to other UCR definitions (aircraft, vehicles,
structures, a person’s identity, watercraft, etc.)
are illogical with 23C=Shoplifting
10) Property descriptions for vehicles and
structures are illogical with 23D=Theft from
Building, 23E=Theft from Coin-Operated
Machine or Device, 23F=Theft from Motor
Vehicle, and 23G=Theft of Motor Vehicle Parts
or Accessories
11) Property descriptions for vehicles are
illogical with 23H=All Other Larceny

__{391}__ `IbrTransformer.BuildPropertyRecordDto`  
Data Element 15 (Property Description) has a code
that requires a zero value in Data Element 16
(Value of Property). Either the wrong property
description code was entered or the property value
was not entered. (This error was formerly error
number 340, a warning message.)
Data Element 16 (Value of Property) must be zero
when Data Element 15 (Property Description) is:
- 09=Credit/Debit Cards
- 22=Nonnegotiable Instruments
- 48=Documents–Personal or Business
- 65=Identity Documents
- 66=Identity–Intangible

__{392}__ `PropertyDrugTypeValidationRule.Validate`  
An offense of 35A Drug/Narcotic Violations and
Data Element 14 (Type Property Loss/Etc.) with
1=None were entered but Data Element 20
(Suspected Drug Type) was not submitted. Since a
drug seizure did not occur, the suspected drug
type must also be entered.
Exception 1 - The incident consists of multiple
offense segments where at least one offense code
is not 35A.
Exception 2 - The incident consists of multiple
property segments where at least one property
segment contains 6 = Seized in Data Element 14
(Type Property Loss/Etc.), and 10 =
Drugs/Narcotics in Data Element 15 (Property
Description).

__{402}__ `VictimRecordIbrFormatter.FormatVictimRecord`  
Must contain numeric entry with zero left-fill.

__{406}__ `IbrTransformer.BuildVictimRecord`, `VictimCircumstanceValidationRule.Validate`, `VictimInjuryValidationRule.Validate`  
The referenced data element in error is one that
contains multiple data values. When more than one
code is entered, none can be duplicate codes.

__{407}__ `VictimInjuryValidationRule.Validate`  
Data Element 33 (Type Injury) Can have multiple
data values and was entered with multiple values.
However, the entry shown between the brackets in
[value] above cannot be entered with any other
data value.

__{408}__ `VictimRecordIbrFormatter.FormatVictimRecord`  
Data Element 26 (Age of Victim) contains data, but
is not left-justified. A single two-character age must
be in positions 1 and 2 of the field.

__{409}__ `VictimRecordIbrFormatter.FormatVictimRecord`  
Data Element 26 (Age of Victim) contains more
than two characters indicating a possible agerange was being attempted. If so, the field must
contain numeric entry of four digits.

__{410}__ `VictimAgeValidationRule.Validate`   
Data Element 26 (Age of Victim) was entered as an
age-range. Accordingly, the first age component
must be less than the second age.

__{419}-1__ `VictimCircumstanceValidationRule.Validate`, `IbrTransformer.BuildVictimRecord`  
Data Element 31 (Aggravated Assault/Homicide
Circumstances) can only be entered when one or
more of the offenses in Data Element 24 (Victim
Connected to UCR Offense Code) are:
- 09A = Murder and Non-negligent Manslaughter
- 09B = Negligent Manslaughter
- 09C = Justifiable Homicide
- 13A = Aggravated Assault

__{419}-2__ `VictimInjuryValidationRule.Validate`  
Data Element 33 (Type Injury) can only be entered
when one or more of the offenses in Data Element
24 (Victim Connected to UCR Offense Code) are:
- 100 = Kidnapping/Abduction
- 11A = Rape
- 11B = Sodomy
- 11C = Sexual Assault With An Object
- 11D = Fondling
- 120 = Robbery
- 13A = Aggravated Assault
- 13B = Simple Assault
- 210 = Extortion/Blackmail
- 64A = Human Trafficking, Commercial Sex Acts
- 64B = Human Trafficking, Involuntary Servitude

__{422}__ `VictimAgeValidationRule.Validate`  
Data Element 26 (Age of Victim) was entered as an
age-range. Therefore, the first age component
cannot be 00 (unknown).

__{450}__ `VictimAgeValidationRule.Validate`  
Data Element 26 (Age of Victim) cannot be less
than 13 years old when Data Element 35
(Relationship of Victim to Offender) contains a
relationship of SE = Spouse.

__{451}__ `VictimSequenceNumberValidationRule.Validate`  
When a Group “A” Incident Report is submitted, the
individual segments comprising the incident cannot
contain duplicates. In this case, two victim
segments were submitted having the same entry in
Data Element 23 (Victim Sequence Number).

__{452}__ `VictimAgeValidationRule.Validate`  
(Age of Victim) must be 17 or greater and less than
or equal to 98 or 00 = Unknown, when Data
Element 25 (Type of Victim) is L = Law
Enforcement Officer. (DE26 must be >=17 & <=98
or DE26 == 00)

__{453}__ `IbrAgeFormatter.FormatIbrVictim`  
The Data Element associated with this error must
be present when Data Element 25 (Type of Victim)
is I = Individual.

__{454}__ `VictimLeokaCircumstanceValidationRule.Validate`, `VictimLeokaAssignmentTypeValidationRule.Validate`, `VictimAgeValidationRule.Validate`, `VictimGenderValidationRule.Validate`, `VidctimRaceValidationRule.Validate`  
Data Element 25A (Type of Officer
Activity/Circumstance), Data Element 25B (Officer
Assignment Type), Data Element 26 (Age of
Victim), Data Element 27 (Sex of Victim), and Data
Element 28 (Race of Victim) must be entered when
Data Element 25 (Type of Victim) is L = Law
Enforcement Officer.

__{455}__ `VictimHomicideCircumstanceValidationRule.Validate`  
Data Element 31 (Aggravated Assault/Homicide
Circumstances) contains: 20 = Criminal Killed by
Private Citizen Or 21 = Criminal Killed by Police
Officer, but Data Element 32 (Additional Justifiable
Homicide Circumstances) was not entered.

__{456}__ `VictimCircumstanceValidationRule.Validate`  
Data Element 31 (Aggravated Assault/Homicide
Circumstances) was entered with two entries, but
was rejected for one of the following reasons:
1) Value 10 = Unknown Circumstances is mutually
exclusive with any other value.
2) Multiple values from more than one category
were entered.
3) Values from the category 09B = Negligent
Manslaughter are mutually exclusive with any other
value.
4) Values from the category 09C = Justifiable
Homicide are mutually exclusive with any other
value.

__{457}__ `VictimHomicideCircumstanceValidationRule.Validate`  
Data Element 32 (Additional Justifiable Homicide
Circumstances) was entered, but Data Element 31
(Aggravated Assault/Homicide Circumstances)
does not reflect a justifiable homicide
circumstance.

__{458}__ `IbrTransformer.BuildVictimRecord`, `VictimAgeValidationRule.Validate`, `VictimGenderValidationRule.Validate`, `VictimEthnicityValidationRule.Validate`, `VictimResidentStatusValidationRule.Validate`, `VictimInjuryValidationRule.Validate`, `OffenderVictimRelationshipValidationRule.Validate`  
The Data Element associated with this error cannot
be entered when Data Element 25 (Type of Victim)
is not I = Individual or L = Law Enforcement Officer

__{459}__ `IbrTransformer.BuildVictimRecord`, `OffenderVictimRelationshipValidationRule.Validate`  
Data Element 34 (Offender Numbers To Be
Related) was entered but should only be entered if
one or more of the offenses entered into Data
Element 24 [Victim Connected to UCR Offense
Code(s)] is a Crime Against Person or is a Robbery
Offense (120). None of these types of offenses
were entered.

__{460}__ - *Handled by Input Form*  
Corresponding Data Element 35 (Relationship of
Victim to Offenders) data must be entered when
Data Element 34 (Offender Numbers To Be
Related) is entered with a value greater than 00.

__{462}__ `VictimCircumstanceValidationRule.Validate`  
Data Element 31 (Aggravated Assault/Homicide
Circumstances) can only have codes of 01 through
06 and 08 through 10 when Data Element 24
(Victim Connected To UCR Offense) is 13A =
Aggravated Assault. All other codes, including 07 =
Mercy Killing, are not valid because they do not
relate to an aggravated assault.

__{463}__ `VictimCircumstanceValidationRule.Validate`  
When a Justifiable Homicide is reported, Data
Element 31 (Aggravated Assault/Homicide
Circumstances) can only have codes of 20 =
Criminal Killed by Private Citizen or 21 = Criminal
Killed by Police Officer. In this case, a code other
than the two mentioned was entered.

__{464}__ `VictimOffenseLinkValidationRule.Validate`  
(Type of Victim) Must have a value of I = Individual
or L = Law Enforcement Officer when Data
Element 24 (Victim Connected to UCR Offense
Code) contains a Crime Against Person.

__{465}__ `VictimOffenseLinkValidationRule.Validate`  
(Type of Victim) Must have a value of S =
Society/Public when Data Element 24 (Victim
Connected to UCR Offense Code) contains a
Crime Against Society.

__{466}__ `IbrTransformer.BuildVictimRecord`  
Each UCR Offense Code entered into Data
Element 24 (Victim Connected to UCR Offense
Codes) must have the Offense Segment for the
value. In this case, the victim was connected to
offenses that were not submitted as Offense
Segments. A victim cannot be connected to an
offense when the offense itself is not present.

__{467}__ `VictimOffenseLinkValidationRule.Validate`  
(Type of Victim) Cannot have a value of S =
Society/Public when Data Element 24 (Victim
Connected to UCR Offense Code) contains a
Crime Against Property.

__{468}__ - *Handled by Input Form*  
Data Element 35 (Relationship of Victim to
Offenders) cannot be entered when Data Element
34 (Offender Number to be Related) is zero. Zero
means that the number of offenders is unknown;
therefore, the relationship cannot be entered.

__{469}__ `VictimGenderValidationRule.Validate`  
Data Element 27 (Sex of Victim) must be M = Male
or F = Female to be connected to offense codes of
11A = Rape and 36B = Statutory Rape.

__{470}__ `OffenderVictimRelationshipValidationRule.Validate`  
Data Element 35 (Relationship of Victim to
Offenders) has a relationship of VO = Victim Was
Offender. When this code is entered, a minimum of
two victim and two offender segments must be
submitted. In this case, only one victim and/or one
offender segment was submitted. The entry of VO
on one or more of the victims indicates situations
such as brawls and domestic disputes. In the vast
majority of cases, each victim is also the offender;
therefore, every victim record would contain a VO
code. However, there may be some situations
where only one of the victims is also the offender,
but where the other victim(s) is not also the
offender(s).

__{471}__ `OffenderVictimRelationshipValidationRule.Validate`  
Data Element 35 (Relationship of Victim to
Offenders) has relationships of VO = Victim Was
Offender that point to multiple offenders, which is
an impossible situation. A single victim cannot be
two offenders.

__{472}__ `IbrTransformer.BuildVictimRecord`  
If Data Element 37 (Age of Offender) is 00 =
Unknown, Data Element 38 (Sex of Offender) is U
= Unknown, and Data Element 39 (Race of
Offender) is U = Unknown, then Data Element 35
(Relationship of Victim to Offender) must be RU =
Relationship Unknown.

__{474}__ `OffenderVictimRelationshipValidationRule.Validate`  
Segment Level 4 (Victim Segment) cannot be
submitted multiple times with VO = Victim Was
Offender in Data Element 35 (Relationship of
Victim to Offender) when Data Element 34
(Offender Number to be Related) contains the
same data value (indicating the same offender).

__{475}__ `OffenderVictimRelationshipValidationRule.Validate`  
A victim can only have one spousal relationship. In
this instance, the victim has a relationship of SE =
Spouse to two or more offenders.

__{476}__ `OffenderVictimRelationshipValidationRule.Validate`  
An offender can only have one spousal
relationship. In this instance, two or more victims
have a relationship of SE = Spouse to the same
offender.

__{477}__ `VictimCircumstanceValdationRule.Validate`  
A victim segment was submitted with Data Element
24 (Victim Connected to UCR Offense Code)
having an offense that does not have a permitted
code for Data Element 31 (Aggravated
Assault/Homicide Circumstances).

__{478}__ `VictimOffenseLinkValidationRule.Validate`, `IbrCodeMutexBusiness.RemoveMutuallyExclusive`  
Mutually Exclusive offenses are ones that cannot
occur to the same victim by UCR definitions. A
Lesser Included offense is one that is an element
of another offense and should not be reported as
having happened to the victim along with the other
offense. Lesser Included and Mutually Exclusive
offenses are defined as follows:
1) Murder-Aggravated assault, simple assault, and
intimidation are all lesser included offenses of murder.
Negligent manslaughter is mutually exclusive.
2) Aggravated Assault-Simple assault and intimidation
are lesser included Note: Aggravated assault is a lesser
included offense of murder, rape, sodomy, sexual
assault with an object, and robbery.
3) Simple Assault-Intimidation is a lesser included
offense of simple assault. Note: Simple assault is a
lesser included offense of murder, aggravated assault,
rape, sodomy, sexual assault with an object, fondling,
and robbery.
4) Intimidation-Intimidation is a lesser included offense of
murder, aggravated assault, rape, sodomy, sexual
assault with an object, fondling, and robbery.
5) Negligent Manslaughter-Murder, aggravated assault,
simple assault, and intimidation are mutually exclusive
offenses. Uniform Crime Reporting Handbook, NIBRS
Edition, page 17, defines negligent manslaughter as
“The killing of another person through negligence.” Page
12 of the same publication shows that assault offenses
are characterized by “unlawful attack[s].” offenses of
aggravated assault.
6) Rape-Aggravated assault, simple assault, intimidation,
and fondling are lesser included offenses of rape. Incest
and statutory rape are mutually exclusive offenses and
cannot occur with rape. The prior two offenses involve
consent, while the latter involves action against the
victim’s will.
7) Sodomy-Aggravated assault, simple assault,
intimidation, and fondling are lesser included offenses of
sodomy. Incest and statutory rape are mutually exclusive
offenses and cannot occur with sodomy. The prior two
offenses involve consent, while the latter involves action
against the victim’s will.
8) Sexual Assault with an Object-Aggravated assault,
simple assault, intimidation, and fondling are lesser
included offenses of sexual assault with an object. Incest
and statutory rape are mutually exclusive offenses and
cannot occur with sexual assault with an object. The
prior two offenses involve consent, while the latter
involves action against the victim’s will.
9) Fondling-Simple assault and intimidation are lesser
included offenses of fondling. Incest and statutory rape
are mutually exclusive offenses and cannot occur with
fondling. The prior two offenses involve consent, while
the latter involves forced action against the victim’s will.
Note: fondling is a lesser included offense of rape,
sodomy, and sexual assault with an object.
10) Incest- rape, sodomy, sexual assault with an object,
and fondling are mutually exclusive offenses. Incest
involves consent, while the prior offenses involve sexual
relations against the victim’s will.
11) Statutory Rape- Rape, sodomy, sexual assault with
an object, and fondling are mutually exclusive offenses.
Statutory rape involves consent, while the prior offenses
involve sexual relations against the victim’s will.
12) Robbery-Aggravated assault, simple assault,
intimidation, and all theft offenses (including motor
vehicle theft) are lesser included offenses of robbery.

__{479}__ `VictimOffenseLinkValidationRule.Validate`  
A Simple Assault (13B) was THE ONLY CRIME
AGAINST PERSON OFFENSE committed against
a victim, but the victim had major injuries/trauma
entered for Data Element 33 (Type Injury). Either
the offense should have been classified as an
Aggravated Assault (13A) or the victim’s injury
should not have been entered as major.

__{480}__ `VictimCircumstanceValidationRule.Validate`  
Data Element 31 (Aggravated Assault/Homicide
Circumstances) has 08 = Other Felony Involved
but the incident has only one offense. For this code
to be used, there must be an Other Felony. Either
multiple entries for Data Element 6 (UCR Offense
Code) should have been submitted, or multiple
individual victims should have been submitted for
the incident report.

__{481}__ `VictimAgeValidationRule.Validate`  
Data Element 26 (Age of Victim) should be under
18 when Data Element 24 (Victim Connected to
UCR Offense Code) is 36B = Statutory Rape.

__{482}__ `VictimOffenseLinkValidationRule.Validate`  
Data Element 25 (Type of Victim) cannot be L =
Law Enforcement Officer unless Data Element 24
(Victim Connected to UCR Offense Code) is one of
the following:
- 09A = Murder & Non-negligent Manslaughter
- 13A = Aggravated Assault
- 13B = Simple Assault
- 13C = Intimidation

__{483}__ `VictimLeokaCircumstanceValidationRule.Validate`, `VictimLeokaAssignmentTypeValidationRule.Validate`, `VictimLeokaOtherOriValidationRule.Validate`  
Data Element 25A (Type of Officer
Activity/Circumstance), Data Element 25B (Officer
Assignment Type), Data Element 25C (Officer–ORI
Other Jurisdiction), can only be entered when Data
Element 25 (Type of Victim) is L = Law
Enforcement Officer.

__{484}__ `VictimRecordIbrFormatter.FormatVictimRecord`  
Segment Length for the Victim Segment (Level 4)
must be 129 characters (not reporting LEOKA) or
141 characters (reporting LEOKA). All Victim
Segments in a submission must be formatted in
only one of these two lengths. ***Note: actual length is 
172 due to VA additions***

__{490}__
(Type of Victim) when Type of Victim is L = Law
Enforcement Officer and Data Element 24 (Victim
Connected to UCR Offense Code) is one of the
following:
- 09A = Murder & Non-negligent Manslaughter
- 13A = Aggravated Assault
- 13B = Simple Assault
- 13C = Intimidation

Data Element 3 (Incident Date/Hour) must be
populated with a valid hour (00-23) and cannot be
blank

__{502}__ `OffenderRecordIbrFormatter.FormatOffenderRecord`  
Data Element 36 (Offender Sequence Number)
must contain numeric entry (00 through 99) with
zero left-fill.

__{508}__ `OffenderRecordIbrFormatter.FormatOffenderRecord`  
Data Element 37 (Age of Offender) contains data
but is not left-justified. A single two-character age
must be in positions 1 through 2 of the field.

__{509}__ `OffenderRecordIbrFormatter.FormatOffenderRecord`  
Data Element 37 (Age of Offender) contains more
than two characters indicating a possible agerange is being attempted. If so, the field must
contain a numeric entry of four digits.

__{510}__ `OffenderAgeValidationRule.Validate`  
Data Element 37 (Age of Offender) was entered as
an age-range. Accordingly, the first age component
must be less than the second age.

__{522}__ `OffenderAgeValidationRule.Validate`  
Data Element 37 (Age of Offender) was entered as
an age-range. Therefore, the first age component
cannot be 00 (unknown).

__{550}__ `OffenderVictimRelationshipValidationRule.IsValidForAges`  
Data Element 37 (age of Offender) cannot be less
than 13 years old when Data Element 35
(Relationship of Victim to Offender) contains a
relationship of SE = Spouse.

__{551}__ `OffenderSequenceNumberValidationRule.Validate`  
When a Group “A” Incident Report is submitted, the
individual segments comprising the incident cannot
contain duplicates. In this case, two Offender
Segments were submitted having the same entry in
Data Element 36 (Offender Sequence Number).

__{552}__ `IbrTransformer.BuildOffenderRecord`  
Data Element 37 (Age of Offender) cannot be
entered when Data Element 36 (Offender
Sequence Number) is 00 = Unknown.

__{554}__ `OffenderVictimRelationshipValidationRule.Validate`  
Data Element 35 (Relationship of Victim to
Offenders) has a relationship that is inconsistent
with the offender’s age. The age of the victim
and/or offender must reflect the implied
relationship. For example, if the relationship of the
victim to offender is PA = Parent, then the victim’s
age must be greater than the offender’s age. The
following relationships must be consistent with the
victim’s age in relation to the offender’s age:
Relationship Victim’s Age Is:
- CH = Victim was Child (Younger)
- PA = Victim was Parent (Older)
- GP = Victim was Grandparent (Older)
- GC = Victim was Grandchild (Younger)

__{555}__ `OffenderSequenceNumberValidationRule.Validate`  
When multiple Offender Segments are submitted,
none can contain a 00 = Unknown value because
the presence of 00 indicates that the number of
offenders is unknown. In this case, multiple
offenders were submitted, but one of the segments
contains the 00 = Unknown value.

__{556}__ `OffenderRecordIbrFormatter.FormatOffenderRecord`  
Data Element 37 (Age of Offender) must contain
numeric entry of 00 through 99

__{557}__ `AdminExceptionalClearanceValidationRule.Validate`  
Data Element 36 (Offender Sequence Number)
contains 00 indicating that nothing is known about
the offender(s) regarding number and any
identifying information. In order to exceptionally
clear the incident, the value cannot be 00. The
incident was submitted with Data Element 4
(Cleared Exceptionally) having a value of A
through E.

__{558}__ `AdminExceptionalClearanceValidationRule.Validate`  
None of the Offender Segments contain all known
values for Age, Sex, and Race. When an Incident
is cleared exceptionally (Data Element 4 contains
an A through E), one offender must have all known
values.

__{559}__ `OffenseUcrCodeValidationRule.Validate`  
The incident was submitted with Data Element 6
(UCR Offense Code) value of 09C = Justifiable
Homicide, but unknown information was submitted
for all the offender(s). At least one of the offenders
must have known information for Age, Sex, and
Race.

__{560}__ `OffenseUcrCodeValidationRule.Validate`  
Segment Level 5 (Offender Segment) must contain
a data value for at least one offender in Data
Element 38 (Sex of Offender) that is not the same
sex that is entered in Data Element 27 (Sex of
Victim) when Data Element 6 (UCR Offense Code)
is 11A = Rape. The offender must be connected to
the victim in Data Element 34 (Offender Number to
be Related) unless offender number in Data
Element 34 (Offender Number to be Related) is 00
= Unknown

__{584}__ `OffenderRecordIbrFormatter.FormatOffenderRecord`  
Segment Length for the Offender Segment (Level 5) 
must be 45 characters (not reporting Offender
Ethnicity) or 46 characters (reporting Offender
Ethnicity). All Offender Segments in a submission
must be formatted in only one of these two lengths.

__{602}__ `GroupAArresteeIbrFormatter.FormatGroupAArrestee`  
Data Element 40 (Arrestee Sequence Number)
must be numeric entry of 01 to 99 with zero leftfill.

__{605}__ `OffenderArrestDateTimeValidationRule.Validate`  
Data Element 42 (Arrest Date) Each component
of the date must be valid; that is, months must be
01 through 12, days must be 01 through 31, and
year must include the century (i.e., 19xx, 20xx). In
addition, days cannot exceed maximum for the
month (e.g., June cannot have 31 days). The date
cannot exceed the current date.
The date cannot be later than that entered within
the Month of Electronic submission and Year of
Electronic submission fields on the data record.
For example, if Month of Electronic submission
and Year of Electronic submission are 06/1999,
the arrest date cannot contain any date
07/01/1999 or later.

__{606}__ `OffenderArmedWithValidationRule.Validate`  
Data Element 46 (Arrestee Was Armed With) The
referenced data element in error is one that
contains multiple data values. When more than
one code is entered, none can be duplicate
codes.

__{607}__ `OffenderArmedWithValidationRule.Validate`  
Data Element 46 (Arrestee Was Armed With) can
have multiple data values and was entered with
multiple values. However, 01 = Unarmed cannot
be entered with any other data value.

__{608}__ `GroupAArresteeIbrFormatter.FormatGroupAArrestee`  
Data Element 47 (Age of Arrestee) contains data,
but is not left-justified. A single two-character age
must be in positions 1 through 2 of the field.

__{609}__ `GroupAArresteeIbrFormatter.FormatGroupAArrestee`  
Data Element 47 (Age of Arrestee) contains more
than two characters indicating a possible agerange is being attempted. If so, the field must
contain a numeric entry of four digits.

__{610}__ `OffenderAgeValidationRule.Validate`  
Data Element 47 (Age of Arrestee) was entered
as an age-range. Accordingly, the first age
component must be less than the second age.

__{618}__ - *Let Fail*  
Data Element 42 (Arrest Date) The UCR Program
has determined that an ORI will no longer be
submitting data to the FBI as of an inactive
date. No arrest data from this ORI will be
accepted after this date.

__{622}__ `OffenderAgeValidationRule.Validate`  
Data Element 47 (Age of Arrestee) was entered
as an age-range. Therefore, the first age
component cannot be 00 (unknown).

__{641}__ `IbrAgeFormatter.FormatIbrArrestee`  
Data Element 47 (Age of Arrestee) was entered
with a value of 99 which means the arrestee was
over 98 years old. Verify that the submitter of data
is not confusing the 99 = Over 98 Years Old with
00 = Unknown.

__{652}__ `OffenderAgeValidationRule.Validate`  
Data Element 52 (Disposition of Juvenile) was not
entered, but Data Element 47 (Age of Arrestee) is
under 18. Whenever an arrestee’s age indicates a
juvenile, the disposition must be entered.

__{653}__ `OffenderDispoUnder18ValidationRule.Validate`  
Data Element 52 (Disposition of Juvenile) was
entered, but Data Element 47 (Age of Arrestee) is
18 or greater. Whenever an arrestee’s age
indicates an adult, the juvenile disposition cannot
be entered because it does not apply.

__{654}__ - *Handled by Dropdown Options*  
Data Element 46 (Arrestee Was Armed With)
does not have A = Automatic or a blank in the
third position of field.

__{655}__  - *Handled by Dropdown Options*  
In Data Element 46 (Arrestee Was Armed With), A
= Automatic is the third character of code. It is
valid only with codes:
- 11 = Firearm (Type Not Stated)
- 12 = Handgun
- 13 = Rifle
- 14 = Shotgun
- 15 = Other Firearm

A weapon code other than those mentioned was
entered with the automatic indicator. An automatic
weapon is, by definition, a firearm.

__{656}__ - *Not Really Possible with unified offender/arrestee records*  
A Group “A” Incident Report was submitted with
more arrestees than offenders. The number (nn)
of offenders is shown within the message. The
incident must be resubmitted with additional
Offender Segments. This message will also occur
if an arrestee was submitted and Data Element 36
(Offender Sequence Number) was 00 = Unknown.
The exception to this rule is when an additional
arrest is reported for a pre-NIBRS incident. In this
case, Segment Level 6 (Arrestee Segment) will
contain A = Add, but the data value in Data
Element 2 (Incident Number) will not match an
incident already on file in the national UCR
database. The segment will be processed, but
used only for SRS purposes and will not be
included in the agency’s NIBRS figures.

__{661}__ `OffenderSequenceNumberValidationRule.Validate`  
Segment Level 6 (Arrestee Segment) cannot
contain duplicate data values in Data Element 40
(Arrestee Sequence Number) when two or more
Arrestee Segments are submitted for the same
incident. The arrest transaction number (DE41) is
the number assigned by the reporting agency to
an arrest report to identify it uniquely. If multiple
arrestees are reported, each having the same
data value in DE40, ensure that DE41 is unique
for each arrestee submitted to ensure that
duplication does not occur.

__{664}__ `IbrAgeFormatter.FormatIbrArrestee`  
Data Element 47 (Age of Arrestee) does not
contain a numeric entry of 00 through 99 for an
exact age.

__{665}__ `OffenderArrestDateTimeValidationRule.Validate`  
Data Element 42 (Arrest Date) cannot be earlier
than Data Element 3 (Incident Date/Hour). A
person cannot be arrested before the incident
occurred.
The exception to this rule is when an additional
arrest is reported for a pre-NIBRS incident. In this
case, Segment Level 6 (Arrestee Segment) will
contain A = Add, but the data value in Data
Element 2 (Incident Number) will not match an
incident already on file in the national UCR
database. The segment will be processed, but
used only for SRS purposes and will not be
included in the agency’s NIBRS figures.

__{667}__ `OffenderGenderValidationRule.Validate`  
Data Element 48 (Sex of Arrestee) does not
contain a valid code of M = Male or F = Female.
Note: U = Unknown (if entered) is not a valid sex
for an arrestee.

__{669}__ `OffenseUcrCodeValidationRule.Validate`  
Group “A” Incident Reports cannot have arrests
when Data Element 6 (UCR Offense Code) is 09C
= Justifiable Homicide. By definition a justifiable
homicide never involves an arrest of the offender
(the person who committed the justifiable
homicide).

__{670}__ `OffenderOffenseLinkValidationRule.Validate`  
Data Element 45 (UCR Arrest Offense Code) was
entered with 09C = Justifiable Homicide. This is
not a valid arrest offense.

__{702}__ `GroupBArresteeIbrFormatter.FormatGroupBArrestee`  
Data Element 40 (Arrestee Sequence Number)
must be numeric entry of 01 to 99 with zero leftfill.

__{705}__ `OffenderArrestDateTimeValidationRule.Validate`  
Data Element 42 (Arrest Date) Each component
of the date must be valid; that is, months must be
01 through 12, days must be 01 through 31, and
year must include the century (i.e., 19xx, 20xx).
In addition, days cannot exceed maximum for the 
month (e.g., June cannot have 31 days). The
date cannot exceed the current date.
The date cannot be later than that entered within
the Month of Electronic submission and Year of
Electronic submission fields on the data record.
For example, if Month of Electronic submission
and Year of Electronic submission are 06/1999,
the arrest date cannot contain any date
07/01/1999 or later.

__{706}__ `OffenderArmedWithValidationRule.Validate`  
Data Element 46 (Arrestee Was Armed With)
cannot contain duplicate data values although
more than one data value is allowed.

__{707}__ `OffenderArmedWithValidationRule.Validate`  
Data Element 46 (Arrestee Was Armed With) can
have multiple data values and was entered with
multiple values. However, 01 = Unarmed cannot
be entered with any other data value.

__{708}__ `GroupBArresteeIbrFormatter.FormatGroupBArrestee`  
Data Element 47 (Age of Arrestee) contains data,
but is not left-justified. A single two-character age
must be in positions 1 through 2 of the field.

__{709}__ `GroupBArresteeIbrFormatter.FormatGroupBArrestee`  
Data Element 47 (Age of Arrestee) contains
more than two characters indicating a possible
age-range is being attempted. If so, the field
must contain numeric entry of four digits.

__{710}__ `OffenderAgeValidationRule.Validate`  
Data Element 47 (Age of Arrestee) was entered
as an age-range. Accordingly, the first age
component must be less than the second age.

__{718}__ - *Let Fail*  
Data Element 42 (Arrest Date) The UCR
Program has determined that an ORI will no
longer be submitting data to the FBI as of an
inactive date. No arrest data from this ORI will be
accepted after this date.

__{720}__ - *Let Fail*  
Group “B” Arrest Report (Level 7) submitted with
a Segment Action Type of A = Add cannot have
Data Element 42 (Arrest Date) earlier than the
Base Date.

__{722}__ `OffenderAgeValidationRule.Validate`  
Data Element 47 (Age of Arrestee) was entered
as an age-range. Therefore, the first age
component cannot be 00 (unknown).

__{741}__ `IbrAgeFormatter.FormatIbrArrestee`  
Data Element 47 (Age of Arrestee) was entered
with a value of 99, which means the arrestee is
over 98 years old. The submitter should verify
that 99 = Over 98 Years Old is not being
confused with 00 = Unknown.

__{751}__ `OffenderSequenceNumberValidationRule`  
(Group B Arrest Report Segment) When a Group
“B” Arrest Report (Level 7) has two or more
arrestees, the individual segments comprising
the report cannot contain duplicates. In this case,
two arrestee segments were submitted having
the same entry in Data Element 40 (Arrestee
Sequence Number). Error should only be thrown
when the action type is A=add.
The arrest transaction number (DE41) is the
number assigned by the reporting agency to an
arrest report to identify it uniquely. If multiple
arrestees are reported, each having the same
data value in DE40, ensure that DE41 is unique
for each arrestee submitted to ensure that
duplication does not occur.

__{752}__ `OffenderAgeValidationRule.Validate`  
Data Element 52 (Disposition of Juvenile) was
not entered, but Data Element 47 (Age of
Arrestee) is under 18. Whenever an arrestee’s
age indicates a juvenile, the disposition must be
entered.

__{753}__`OffenderDispoUnder18ValidationRule.Validate`  
Data Element 52 (Disposition of Juvenile) was
entered, but Data Element 47 (Age of Arrestee)
is 18 or greater. Whenever an arrestee’s age
indicates an adult, the juvenile disposition cannot
be entered because it does not apply.

__{754}__ - *Handled by Dropdown Options*  
Data Element 46 (Arrestee Was Armed With)
does not have A = Automatic or a blank in the
third position of field.

__{755}__ - *Handled by Dropdown Options*  
If Data Element 46 (Arrestee Was Armed With)
weapon is an Automatic, add A as the third
character of code; valid only with codes of:
- 11 = Firearm (Type Not Stated)
- 12 = Handgun
- 13 = Rifle
- 14 = Shotgun
- 15 = Other Firearm
A weapon code other than those mentioned was
entered with the automatic indicator. An
automatic weapon is, by definition, a firearm.

__{757}__ `IbrAgeFormatter.FormatIbrArrestee`  
Data Element 47 (Age of Arrestee) does not
contain a numeric entry of 00 through 99 for an
exact age.

__{758}__ `OffenderGenderValidationRule.Validate`  
Data Element 48 (Sex of Arrestee) does not
contain a valid code of M = Male or F = Female.
Note that U = Unknown (if entered) is not a valid
sex for an arrestee.

__{759}__ `OffenderSequenceNumberValidationRule`  
The Group “B” Arrest Report (Level 7) submitted
as an Add is currently active in the FBI’s
database; therefore, it was rejected. If multiple
arrestees are involved in the incident, ensure that
Data Element 40 (Arrestee Sequence Number) is
unique for each Arrestee Segment submitted so
that duplication does not occur.

__{760}__ `IbrPreProcessor.PreProcessIncident`  
Group “B” Arrest Reports (Level 7) must contain
a Group “B” Offense Code in Data Element 45
(UCR Arrest Offense). The offense code
submitted is not a Group “B” offense code.

__{797}__ - *Arrest date should already be saved when submitted originally*  
Data Element 42 (Arrest Date) is missing for a
Group B Arrest Report with a Segment Action
Type of D = Delete; must be populated with a
valid data value and cannot be blank.

__{798}__ - *Arrest date should already be saved when submitted originally*  
Data Element 42 (Arrest Date) is missing for a
Group B Arrest Report with a Segment Action
Type of D = Delete; at least one Group B Arrest
Report is on file that matches Data Element 41
(Arrest Transaction Number) with an Arrest Date
outside the two year window.

__{799}__ - *Arrest date should already be saved when submitted originally*  
Data Element 42 (Arrest Date) is missing for a
Group B Arrest Report with a Segment Action
Type of D = Delete; multiple Group B Arrest
Reports are on file that match Data Element 41
(Arrest Transaction Number).