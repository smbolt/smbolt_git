       IDENTIFICATION DIVISION.
       PROGRAM-ID.    ORGUT100.
       AUTHOR.        STEPHEN BOLT - INDEPENDENT CONSULTANT.
       DATE-WRITTEN.  08/13/2015.

      ******************************************************************
      * ORGUT100 - FILE SEARCH UTILITY                                 *
      ******************************************************************
      * THIS PROGRAM WILL CONDUCT SEARCHES FOR SPECIFIC CONDITIONS     *
      * IN THE DIRECTORY TARGET PROVIDED VIA THE COMMAND LINE.         *
      ******************************************************************
      *                       PROGRAM HISTORY                          *
      ******************************************************************
      *   DATE     !  PGMR    !     DESCRIPTION OF CHANGE              *
      *------------!----------!----------------------------------------*
      * 08-13-2015 ! SBOLT    ! INITIAL PROGRAM CODING.                *
      *            !          !                                        *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*
                                                                        
       ENVIRONMENT DIVISION.
       CONFIGURATION SECTION.
       SPECIAL-NAMES.
       
       INPUT-OUTPUT SECTION.
       FILE-CONTROL.
           SELECT CONTROL-FILE 
           ASSIGN TO "C:\CONTROL.DAT"
           ORGANIZATION IS SEQUENTIAL.

       DATA DIVISION.
       
       FILE SECTION.
        FD CONTROL-FILE.
        01 CONTROL-RECORD.
           05 FILLER                       PIC X(80).
       
       
       EJECT
      ****************************************************************** 
       WORKING-STORAGE SECTION.
      *************************************************************WSX**
      *--1----+----2----+----3----+----4----+----5----+----6----+----7--
       
       EJECT
      ******************************************************************
      *    COPYCODE FOR STANDARD CONTROL BLOCK                         *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

               COPY 'ORGST001.CPY'.
       
       EJECT
      ******************************************************************
      *    COPYCODE FOR STANDARD SUBPROGRAM LINKAGE                    *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

               COPY 'ORGST002.CPY'.
       
       EJECT
      ******************************************************************
      *    COPYCODE FOR STANDARD LOG SUBPROGRAM WS FIELDS              *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

               COPY 'ORGST003.CPY'.

       EJECT
      ******************************************************************
      *    COPYCODE FOR STANDARD LOG SUBPROGRAM LINKAGE                *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

               COPY 'ORGST004.CPY'.

       EJECT
      ******************************************************************
      *    COPYCODE FOR STANDARD LOG MESSAGES                          *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

               COPY 'ORGST005.CPY'.

       EJECT
      ******************************************************************
      *    COPYCODE FOR STANDARD DATE MODULE WS FIELDS                 *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

               COPY 'ORGST006.CPY'.

       EJECT
      ******************************************************************
      *    COPYCODE FOR STANDARD DATE MODULE LINKAGE                   *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

               COPY 'ORGST007.CPY'.

       EJECT
      ******************************************************************
      *    COPYCODE FOR STANDARD DCCM INTERFACE WS FIELDS              *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

               COPY 'ORGST008.CPY'.

       EJECT
      ******************************************************************
      *    COPYCODE FOR STANDARD DCCM INTERFACE LINKAGE FIELDS         *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

               COPY 'ORGST009.CPY'.
       
       EJECT
      ******************************************************************
      *  THE CODE BELOW IS RELATED TO THE INTERFACE WITH ORACLE        *
      ******************************************************************

      *    EXEC SQL BEGIN DECLARE SECTION END-EXEC.

      *01  USERNAME                      PIC X(10) VARYING.
      *01  PASSWD                        PIC X(10) VARYING.
       01  DBV-PROG-NAME                 PIC X(12).

       01  SQL-ERROR-MSG-TEXT            PIC X(200).
       01  SQL-ERROR-MSG-SIZE            PIC S9(09) COMP VALUE ZEROES.
       01  SQL-ERROR-MSG-LENGTH          PIC S9(09) COMP VALUE ZEROES.

      *    EXEC SQL END DECLARE SECTION END-EXEC.

       EJECT
      *    EXEC SQL INCLUDE SQLCA END-EXEC.
       
       
       EJECT
      ******************************************************************
      *  WORK FIELDS SPECIFIC TO THIS PROGRAM.                         *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

       01  WSC-CONSTANTS.
           05  WSC-ORGUT100              PIC X(08) VALUE 'ORGUT100'.
           05  WSC-ORGUT001              PIC X(08) VALUE 'ORGUT001'.
           05  WSC-ORGUT002              PIC X(08) VALUE 'ORGUT002'.
           05  WSC-ORGUT003              PIC X(08) VALUE 'ORGUT003'.

       01  WSC-VARIABLES.
           05  WSC-COUNTER               PIC 9(04) VALUE ZEROES.
           05  WSC-POINTER               PIC 9(05) VALUE ZEROES.
           05  WSC-TRANTYPE-HOLD         PIC X(06) VALUE SPACES.
           05  WSC-TRANLEN-HOLD          PIC 9(05) VALUE ZEROES.
           05  WSC-TABLE-INDEX           PIC 9(02) VALUE ZEROES.
           05  WSC-DATE-HOLD             PIC 9(08) VALUE ZEROES.
           05  WSC-TIME-HOLD             PIC 9(08) VALUE ZEROES.

       01  WSC-TRANSACTIONS.
           05  WSC-HEADER                PIC 9(04) VALUE ZEROES.
           05  WSC-TX01-HDR-LEN          PIC 99    VALUE 00.
           05  WSC-ORD-LEN               PIC 9(05) VALUE ZEROES.
           05  WSC-CAN-LEN               PIC 9(05) VALUE ZEROES.
           05  WSC-INQ-LEN               PIC 9(05) VALUE ZEROES.
           05  WSC-REF-LEN               PIC 9(05) VALUE ZEROES.
           05  WSC-MES-LEN               PIC 9(05) VALUE ZEROES.
           05  WSC-PRC-LEN               PIC 9(05) VALUE ZEROES.
           05  WSC-GEN-LEN               PIC 9(05) VALUE ZEROES.
           05  WSC-CCD-LEN               PIC 9(05) VALUE ZEROES.
           05  WSC-INC-LEN               PIC 9(05) VALUE ZEROES.
           05  WSC-STP-LEN               PIC 9(05) VALUE ZEROES.
           
       01  LOAD-DLL-PTR                  USAGE PROCEDURE-POINTER.
       
       
       EJECT
      ******************************************************************
      *   AREAS FOR THE MANAGEMENT OF PROGRAM MESSAGES                 *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7--

      * PROGRAM MESSAGES ARE ALLOCATED NUMBERS 101000-101999
       77  PGMMSG-LOW                  PIC 9(06)  VALUE 101000.
       77  PGMMSG-HIGH                 PIC 9(06)  VALUE 101999.
      * CURRENTLY THE HIGHEST NUMBER MESSAGE THAT IS DEFINED
       77  PGMMSG-MAX-USED             PIC 9(06)  VALUE 101006.
       77  PGMMSG-SUB                  PIC S9(05) COMP-3 VALUE +0.

       01  PROGRAM-MESSAGES.
           05  PGMMSG-101000.
               10  FILLER                  PIC X(03) VALUE '08A'.
               10  FILLER                  PIC X(50) VALUE
               'GENERIC MESSAGE FOR CUSTOMIZATION                 '.
               10  FILLER                  PIC X(50) VALUE
               '                                                  '.
           05  PGMMSG-101001.
               10 FILLER                   PIC X(03)  VALUE '16A'.
               10 FILLER                   PIC X(50)  VALUE
               'COULD NOT GET QUEUE MANAGER NAME                  '.
               10  FILLER                  PIC X(50) VALUE
               '                                                  '.

           05  PGMMSG-101002.
               10  FILLER                  PIC X(03) VALUE '16A'.
               10  FILLER                  PIC X(50) VALUE
               'COULD NOT GET NAME FOR INPUT QUEUE                '.
               10  FILLER                  PIC X(50) VALUE
               '                                                  '.

           05  PGMMSG-101003.
               10  FILLER                  PIC X(03) VALUE '16A'.
               10  FILLER                  PIC X(50) VALUE
               'COULD NOT GET NAME FOR OUTPUT QUEUE               '.
               10  FILLER                  PIC X(50) VALUE
               '                                                  '.

           05  PGMMSG-101004.
               10  FILLER                  PIC X(03) VALUE '16A'.
               10  FILLER                  PIC X(50) VALUE
               'FAILED TO CONNECT TO ORACLE                       '.
               10  FILLER                  PIC X(50) VALUE
               '                                                  '.

           05  PGMMSG-101005.
               10  FILLER                  PIC X(03) VALUE '16A'.
               10  FILLER                  PIC X(50) VALUE
               'FAILED TO COMMIT CHANGES TO ORACLE                '.
               10  FILLER                  PIC X(50) VALUE
               '                                                  '.

           05  PGMMSG-101006.
               10  FILLER                  PIC X(03) VALUE '16A'.
               10  FILLER                  PIC X(50) VALUE
               'FAILED TO SIGNOFF OF ORACLE                       '.
               10  FILLER                  PIC X(50) VALUE
               '                                                  '.
       01  FILLER REDEFINES PROGRAM-MESSAGES.
      *    THE OCCURS CLAUSE MUST BE KEPT IN SYNCH WITH THE NUMBER
      *    OF MESSAGES THAT ARE DEFINED ABOVE.
           05  FILLER OCCURS 07.
               10 LOG-SEVERITY         PIC 9(02).
               10 LOG-SOURCE           PIC X.
               10 PGMMSG               PIC X(100).
       
       
      ****************************************************************** 
       LINKAGE SECTION.
      *************************************************************LX***
      *--1----+----2----+----3----+----4----+----5----+----6----+----7--
       
       
      ****************************************************************** 
       PROCEDURE DIVISION.
      *************************************************************PX***
      *--1----+----2----+----3----+----4----+----5----+----6----+----7--
                                                                        
       00000-PROGRAM-ORGUT001.
       
           PERFORM 10000-PROGRAM-INITIALIZATION THRU 10000-EXIT.
           PERFORM 40000-MAIN-PROCESSING THRU 40000-EXIT
                   UNTIL TERMINATE-PROGRAM.
           PERFORM 90000-PROGRAM-TERMINATION THRU 90000-EXIT.
           GOBACK.


       EJECT
      ******************************************************************
      * 10000  PARAGRAPHS IN THIS LEVEL PERFORM THE INITIALIZATION     *
      *        FUNCTIONS FOR THIS PROGRAM.                             *
      *************************************************************PX1**
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*
                                                                        
       10000-PROGRAM-INITIALIZATION.
           PERFORM 10010-SET-UP THRU 10010-EXIT.
           PERFORM U1000-STANDARD-EXE-INIT THRU U1000-EXIT.

           IF CONTINUE-PROCESS
               PERFORM 10100-GET-CONFIG-VALUES THRU 10100-EXIT
           END-IF.
       10000-EXIT.
           EXIT.

       10010-SET-UP.
           MOVE WSC-ORGUT100             TO SCB-PROGRAM-NAME.
           MOVE STC-0                    TO SCB-SLEEP-DURATION.
           MOVE STC-0                    TO SCB-SLEEP-INCREMENT.
           MOVE STC-0                    TO SCB-SLEEP-LIMIT.
           MOVE STC-0                    TO SCB-SLEEP-COUNT.
           MOVE 050                      TO SCB-DCCM-INTERVAL.
           MOVE STC-0                    TO SCB-DCCM-COUNT.
           MOVE STC-N                    TO SCB-DB-CONNECTED.
           MOVE STC-0                    TO SCB-PACE-INTERVAL.
           MOVE STC-0                    TO SCB-PACE-DURATION.
           MOVE 60                       TO SCB-PAUSE-DURATION.
       10010-EXIT.
           EXIT.

       10100-GET-CONFIG-VALUES.
           MOVE STC-DCCM-INTERVAL-LIT    TO SCB-CONFIG-NAME.
           PERFORM U1200-GET-CONFIG-VALUE THRU U1200-EXIT.
           IF CONFIG-VALUE-FOUND
               MOVE FUNCTION NUMVAL(ST009-CONFIG-VALUE)
                                         TO SCB-DCCM-INTERVAL
           END-IF.

           MOVE STC-PACE-INTERVAL-LIT    TO SCB-CONFIG-NAME.
           PERFORM U1200-GET-CONFIG-VALUE THRU U1200-EXIT.
           IF CONFIG-VALUE-FOUND
               MOVE FUNCTION NUMVAL(ST009-CONFIG-VALUE)
                                         TO SCB-PACE-INTERVAL
           END-IF.

           MOVE STC-PACE-DURATION-LIT    TO SCB-CONFIG-NAME.
           PERFORM U1200-GET-CONFIG-VALUE THRU U1200-EXIT.
           IF CONFIG-VALUE-FOUND
               MOVE FUNCTION NUMVAL(ST009-CONFIG-VALUE)
                                         TO SCB-PACE-DURATION
           END-IF.

           MOVE STC-PAUSE-DURATION-LIT    TO SCB-CONFIG-NAME.
           PERFORM U1200-GET-CONFIG-VALUE THRU U1200-EXIT.
           IF CONFIG-VALUE-FOUND
               MOVE FUNCTION NUMVAL(ST009-CONFIG-VALUE)
                                         TO SCB-PAUSE-DURATION
           END-IF.
       10100-EXIT.
           EXIT.

       
       
      ****************************************************************** 
      * 40000-MAIN-PROCESSING - THIS PARAGRAPH PERFORMS THE MAIN       *
      *       PROCESSING FUNCTIONS FOR THE PROGRAM.                    *                                                                 
      *************************************************************PX4** 
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-- 
         
       40000-MAIN-PROCESSING.
           DISPLAY "*******************************************".
           DISPLAY "* 40000 MAIN                               *".
           DISPLAY "*******************************************".
           DISPLAY " ".
           
           MOVE STC-Y                    TO SCB-TERMINATE-PROGRAM.
           
       40000-EXIT.
           EXIT.
         
       
       EJECT
      ******************************************************************
      * 80000  PARAGRAPHS IN THIS LEVEL PERFORM PROGRAM SPECIFIC       *
      *        MISCELLANEOUS FUNCTIONS.                                *
      *************************************************************PX8**
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

       81000-RESUME.
      ******************************************************************
      *==*  THIS PARAGRAPH IS REQUIRED IN ORDER FOR PROGRAMS THAT   *==*
      *==*  USE THE STANDARD EXECUTABLE PROGRAM INITIALIZATION AND  *==*
      *==*  TERMINATION ROUTINES (COPYBOOK ORGPX000) TO COMPILE.    *==*
      *==*                                                          *==*
      *==*  THIS PARAGRAPH IS PERFORMED FROM PARAGRAPH U1300-CHECK- *==*
      *==*  DCCM WHEN THE DCCM CONTROL VARIABLE FOR THE RUNNING     *==*
      *==*  INSTANCE OF THIS PROGRAM HAS BEEN CHANGED FROM "PAUSE"  *==*
      *==*  TO "RESUME".  INCLUDE ANY PROGRAM RESUMPTION PROCESSING *==*
      *==*  NECESSARY IN THIS PARAGRAPH.  AFTER THIS PARAGRAPH IS   *==*
      *==*  PERFORMED, THE DCCM CONTROL VARIABLE WILL BE RESET TO   *==*
      *==*  "NORMAL" BY SUBSEQUENT PROCESSING IN PARAGRAPH 1300-    *==*
      *==*  CHECK-DCCM.                                             *==*
      *=****************************************************************

      *     CODE ANY PROGRAM RESUMPTION PROCESSING HERE    *

       81000-EXIT.
           EXIT.

       EJECT

       82000-RECONFIG.
      *    PERFORM 10010-SET-UP THRU 10010-EXIT.
      *    PERFORM 10100-GET-CONFIG-VALUES THRU 10100-EXIT.
       82000-EXIT.
           EXIT.
         
         
       EJECT    
      ****************************************************************** 
      * 90000-PROGRAM-TERMINATION - THIS PARAGRAPH PERFORMS ALL        *
      *       TERMINATION-RELATED FUNCTIONS FOR THE PROGRAM.           *                                                                 
      *************************************************************PX9**
      *--1----+----2----+----3----+----4----+----5----+----6----+----7--  
           
       90000-PROGRAM-TERMINATION.
           DISPLAY "*******************************************".
           DISPLAY "* 90000 TERMINATION                       *".
           DISPLAY "*******************************************".
           DISPLAY " ".
       90000-EXIT.
           EXIT.
       
       EJECT
      ******************************************************************
      * D0000  PARAGRAPHS IN THIS LEVEL PERFORM STANDARD ORACLE        *
      *        DATABASE RELATED FUNCTIONS.                             *
      ******************************************************************
       
       D8000-COMMIT.

           MOVE STC-DB-OK                TO SCB-DB-RESULT.

      *    EXEC SQL
      *        COMMIT WORK
      *    END-EXEC.

      *    IF SQLCODE NOT = STC-SQLCODE-OK
      *        MOVE STC-DB-FAILED        TO SCB-DB-RESULT
      *        MOVE 106035 TO SCB-REASON-CODE
      *        PERFORM U9900-LOG THRU U9900-EXIT
      *        PERFORM D9999-DATABASE-ERROR THRU D9999-EXIT
      *    END-IF.

       D8000-EXIT.
           EXIT.
       
       
       D9999-DATABASE-ERROR.

      *    IF DB-CONNECTED
      *        EXEC SQL ROLLBACK WORK RELEASE END-EXEC
      *    END-IF.

           MOVE STC-Y                    TO SCB-TERMINATE-PROGRAM.
           MOVE STC-N                    TO SCB-CONTINUE-PROCESS.
           MOVE STC-N                    TO SCB-DB-CONNECTED.

       D9999-EXIT.
           EXIT.
       
       
           
       EJECT
      ******************************************************************
      * U0000  PARAGRAPHS IN THIS LEVEL PERFORM UTILITY FUNCTIONS      *
      *        FOR THIS PROGRAM.                                       *
      *************************************************************PXU**
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*


      ******************************************************************
      *  COPYCODE FOR STANDARD "EXECUTABLE TYPE" INITIALIZATION        *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

               COPY 'ORGPX001.CPY'.
               
               
       EJECT
      ******************************************************************
      *  COPYCODE FOR STANDARD SLEEP FUNCTION                          *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

               COPY 'ORGPX002.CPY'.

       EJECT
      ******************************************************************
      *  COPYCODE FOR STANDARD LOGGING FUNCTION                        *
      ******************************************************************
      *--1----+----2----+----3----+----4----+----5----+----6----+----7-*

               COPY 'ORGPX003.CPY'.