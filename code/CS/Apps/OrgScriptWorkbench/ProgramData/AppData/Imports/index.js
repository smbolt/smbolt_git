'use strict';
app.controller('railOpsController', ['$rootScope', '$scope', '$q', '$location', '$timeout', '$routeParams', 'appService', 'railOpsService', 'terminalService', 'trackService', 'modalService', '$modal', 'localStorageService',
  function ($rootScope, $scope, $q, $location, $timeout, $routeParams, appService, railOpsService, terminalService, trackService, modalService, $modal, localStorageService) {

    var writeLog = function (logMessage) {
      appService.writeLog(logMessage);
    }

    $scope.noEntitiesMessage = '';
    $scope.trackNameList = new Array();
    $scope.terminalNameList = new Array();

    $scope.selectedTerminal = '';
    $scope.selectedTrack = '';

    $scope.inDiagnosticsMode = function () {
      return appService.getDiagnosticsMode();
    }

    $scope.clearLog = function () {
      appService.clearLog();
    }

    $scope.getLog = function () {
      return appService.getLog();
    }

    writeLog('SCRIPT LOADED');

    var defaultTerminalId = appService.getDefaultTerminalId();

    var lsTerminalId = localStorageService.get('railOpsTerminalId');
    var lsTrackId = localStorageService.get('railOpsTrackId');

    if (lsTerminalId == null || lsTerminalId == 0) {
      $scope.terminalId = defaultTerminalId;
      localStorageService.set('railOpsTerminalId', $scope.terminalId);
      writeLog('TermId not in LS, setting to user default ' + defaultTerminalId + ', storing to LS');
    }
    else {
      $scope.terminalId = lsTerminalId;
      writeLog('TermId in LS, value is ' + $scope.terminalId);
    }

    if (lsTrackId == null) {
      $scope.trackId = 0;
      localStorageService.set('railOpsTrackId', $scope.trackId);
      writeLog('TrackId not in LS, setting to 0, storing in LS');
    }
    else {
      $scope.trackId = lsTrackId;
      writeLog('TrackId in LS, value is ' + $scope.trackId);
    }


    var initializing = true;
    var startWatching = true;
    initializeSpottedCars($scope, $routeParams);
    var beginShowingErrors = false;
    $scope.showValidation = $scope.editMode == 'new' ? beginShowingErrors : true;

    $scope.authenticatedUserId = appService.authentication.authenticatedUserId;
    $scope.authenticatedOrganizationId = appService.authentication.authenticatedOrganizationId;
    var orgId = $scope.authenticatedOrganizationId;
    $scope.authenticatedUserName = appService.authentication.authenticatedUserName;
    var defaultTerminalId = appService.getDefaultTerminalId();
    var token = appService.authentication.token;
    $("#userName").html("UserName: " + $scope.authenticatedUserName);


    $scope.handleTerminalChange = function () {
      $scope.noEntitiesMessage = '';
      writeLog('entered handleTerminalChange');
      for (var i = 0; i < $scope.terminalList.length; i++) {
        if ($scope.terminalList[i].terminalName == $scope.selectedTerminal) {
          $scope.terminalId = $scope.terminalList[i].terminalId;
          localStorageService.set('railOpsTerminalId', $scope.terminalId);
          writeLog('in handleTerminalChange: terminalList item matched by name ' + $scope.selectedTerminal + ' to be TerminalID ' + $scope.terminalId);
          railOpsService.getAllTracksForTerminal($scope.terminalId, token).then(function (response) {
            var gotTrackList = false;
            var status = response.status;
            if (status == 'OK') {
              $scope.trackList = response.object;
              writeLog('in handleTerminalChange, successful retrieval of trackList for TerminalId ' + $scope.terminalId + ' ' + $scope.trackList.length + ' tracks retrieved');
              if ($scope.trackList.length > 0) {
                gotTrackList = true;
                var theTrack = $scope.trackList[Object.keys($scope.trackList)[0]];
                getTrackNameList($scope.trackList);
                $scope.trackId = theTrack.trackId;
                $scope.selectedTrack = theTrack.trackName;
                localStorageService.set('railOpsTrackId', $scope.trackId);
                writeLog('in handleTerminalChange, storing TrackId ' + $scope.trackId + ' in LS');
                if ($scope.trackId == null) {
                  writeLog('in handleTerminalChange, TrackId ' + $scope.trackId + ' not found in list of tracks for TerminalId ' + $scope.terminalId);
                  $scope.trackId = 0;
                }
                else {
                  writeLog('in handleTerminalChange, TrackId ' + $scope.trackId + ' successfully found for TerminalId ' + $scope.terminalId);
                }
                writeLog('in handleTerminalChange, calling getSpottedCars');
                getSpottedCars();
              }
              else {
                $scope.noEntitiesMessage = 'No tracks are defined for ' + $scope.selectedTerminal;
              }
            }

            if (!gotTrackList) {
              $scope.spottedCars = [];
              $scope.trackNameList = new Array();
              $scope.selectedTrack = '';
              $scope.trackId = 0;
              localStorageService.set('railOpsTrackId', 0);
            }
          });
        }
      }
    };

    $scope.handleTrackChange = function () {
      writeLog('entered handleTrackChange');
      for (var i = 0; i < $scope.trackList.length; i++) {
        var track = $scope.trackList[Object.keys($scope.trackList)[i]];
        if (track.trackName == $scope.selectedTrack) {
          $scope.trackId = track.trackId;
          localStorageService.set('railOpsTrackId', $scope.trackId);
          writeLog('in handleTrackChange, storing TrackId ' + $scope.trackId + ' in LS');
          writeLog('in handleTrackChange: trackList item matched by name ' + $scope.selectedTrack + ' to be TrackID ' + $scope.trackId);
          writeLog('in handleTrackChange: calling getSpottedCars');
          getSpottedCars();
          return;
        }
      }
    };

    var getTerminalNameList = function (terminalList) {
      $scope.terminalNameList = new Array();
      if (terminalList != null) {
        var terminalNameFound = false;
        writeLog('in getTerminalNameList, ' + terminalList.length + ' terminals being processed');
        for (var i = 0; i < terminalList.length; i++) {
          $scope.terminalNameList[i] = terminalList[i].terminalName;
          if ($scope.terminalId == terminalList[i].terminalId) {
            $scope.selectedTerminal = terminalList[i].terminalName;
            writeLog('in getTerminalNameList, found name for TerminalId ' + $scope.terminalId + ': ' + $scope.selectedTerminal);
            terminalNameFound = true;
          }
        }
        if (!terminalNameFound) {
          writeLog('### in getTerminalNameList, name NOT FOUND for TerminalId ' + $scope.terminalId);
        }
      }
    };

    var getTrackNameList = function (trackList) {
      $scope.selectedTrack = '';
      $scope.trackNameList = new Array();
      if (trackList != null) {
        var trackNameFound = false;
        writeLog('in getTrackNameList, ' + trackList.length + ' tracks being processed');
        for (var i = 0; i < trackList.length; i++) {
          $scope.trackNameList[i] = trackList[Object.keys(trackList)[i]].trackName;
        }
      }
    };

    // get terminal list
    $scope.terminalList = appService.getTerminalList();
    if (!$scope.terminalList) {
      terminalService.getAllTerminals(orgId, token).then(function (response) {
        var apiResult = response.data;
        var status = apiResult.apiStatus.toLowerCase();
        var code = apiResult.code;
        var message = apiResult.message;
        var pagingControl = apiResult.pagingControl;
        var longMessage = apiResult.longMessage;
        var token = apiResult.token;
        switch (status) {
          case 'success':
            appService.updateSecurityToken(token);
            $scope.terminalList = apiResult.responseData;
            appService.setTerminalList($scope.terminalList);
            getTerminalNameList($scope.terminalList);
            break;
        }
      });
    }
    else {
      getTerminalNameList($scope.terminalList);
    }


    // get list of spotted cars (rail ops list)
    var getSpottedCars = function () {
      writeLog('in getSpottedCars TerminalId is ' + $scope.terminalId + ' TrackId is ' + $scope.trackId);
      railOpsService.getSpottedCars($scope.terminalId, $scope.trackId, $scope.page, $scope.perPage, token).then(function (response) {
        var apiResult = response.data;
        var status = apiResult.apiStatus.toLowerCase();
        var code = apiResult.code;
        var message = apiResult.message;
        var pagingControl = apiResult.pagingControl;
        var longMessage = apiResult.longMessage;
        var token = apiResult.token;
        switch (status) {
          case 'success':
            appService.updateSecurityToken(token);
            $scope.spottedCars = apiResult.responseData;
            setPossibleActions($scope.spottedCars);
            $scope.lastPage = Math.ceil(pagingControl.totalEntityCount / $scope.perPage);
            if ($scope.lastPage == 0) $scope.lastPage = 1;
            $scope.totalSpottedCars = pagingControl.totalEntityCount;
            if ($scope.totalSpottedCars == 0) {
              $scope.noEntitiesMessage = 'No spots are defined for ' + $scope.selectedTrack + ' on ' + $scope.selectedTerminal;
            }
            else {
              $scope.noEntitiesMessage = '';
            }
            $scope.firstDisabled = $scope.page == 1;
            $scope.prevDisabled = $scope.page == 1;
            $scope.nextDisabled = $scope.page == $scope.lastPage;
            $scope.lastDisabled = $scope.page == $scope.lastPage;
            startWatching = true;
            break;
        }
      });
    };

    var setPossibleActions = function (spottedCars) {
      for (var i = 0; i < spottedCars.length; i++) {
        var curStatus = spottedCars[i].railOpsStatus;
        spottedCars[i].nextActionList = getNextActionList(curStatus.toString().toLowerCase());
      }
    }

    var getNextActionList = function (curStatus) {
      var actionList = new Array();
      switch (curStatus) {
        case 'open':
          actionList[1] = 'Spot';
          break;
        case 'spotted':
          actionList[1] = 'Pre-Insp';
          actionList[2] = 'Post-Insp';
          actionList[3] = 'Release';
          break;
        case 'pre-insp [p]':
          actionList[1] = 'Pre-Insp';
          actionList[2] = 'Post-Insp';
          actionList[3] = 'Release';
          break;
        case 'pre-insp [f]':
          actionList[1] = 'Pre-Insp';
          actionList[2] = 'Post-Insp';
          actionList[3] = 'Release';
          break;
        case 'loading':
        case 'loaded':
          actionList[1] = 'Post-Insp';
          actionList[2] = 'Release';
          break;
        case 'post-insp [p]':
        case 'post-insp [f]':
          actionList[1] = 'Release';
          break;
        default:
          actionList[1] = 'Spot';
          actionList[2] = 'Pre-Insp';
          actionList[3] = 'Post-Insp';
          actionList[4] = 'Release';
          break;
      }
      return actionList;
    };

    $scope.showLoadUnitsByTankCar = function (index) {
      localStorageService.set('loadUnitReferrer', 'railOps');
      var spottedCar = $scope.spottedCars[index];
      var tankCarId = spottedCar.tankCarId;
      $location.path('/loadUnitTankCar/' + tankCarId);
    }

    $scope.railOpsAction = function (index) {
      var spottedCar = $scope.spottedCars[index];

      var action = spottedCar.defaultNextAction.toString().toLowerCase().trim();
      var spotId = spottedCar.spotId;
      var spottedCarId = spottedCar.spottedCarId;
      var terminalId = $scope.terminalId;
      var trackId = $scope.trackId;
      var spotName = spottedCar.spotName;
      var currentOpsStatus = spottedCar.railOpsStatus;

      $scope.actionEvent = 'Action=' + action + ' SpotId=' + spotId + ' SpotName=' + spotName + ' SpottedCarId=' + spottedCarId +
        ' TerminalId=' + terminalId + ' TrackId=' + trackId + ' Index=' + index;

      switch (action) {
        case "spot":
          locateTankCar(spotId);
          break;

        case "pre-insp":
          $location.path('/preInspection/' + spotId);
          break;

        case "load":
          $location.path('/loading/' + spotId);
          break;

        case "post-insp":
          $location.path('/postInspection/' + spotId);
          break;

        case "release":
          releaseSpottedCar(spotId);
          break;
      }

    }

    $scope.modalControl = {
      spotId: null,
      orgId: orgId
    };

    var locateTankCar = function (spotId) {
      var scope = $rootScope.$new();
      scope.params = {
        selectedStatus: 'Active',
        bgColor: '#1d7dcf',
        headerText: 'Locate Tank Car for Spot'
      };

      $scope.modalControl.spotId = spotId;

      $modal.open({
        scope: scope,
        templateUrl: '/app/views/dialogs/modalLocateTankCar.html',
        windowClass: 'medium',
        controller: locateRailCarModalController,
        resolve: {
          modalControl: function () {
            return $scope.modalControl;
          }
        }
      }).result.then(function () {
        startTimer();
      });
    };

    var releaseSpottedCar = function (spotId) {
      var apiInput = {};
      apiInput.apiAction = 'new';
      apiInput.userId = $scope.authenticatedUserId;
      apiInput.code = 0;
      apiInput.command = '';
      apiInput.value = {};
      apiInput.value.spotId = spotId;

      railOpsService.releaseSpottedCar(apiInput, token).then(function (response) {
        var apiResult = response.data;
        var status = apiResult.apiStatus.toLowerCase();
        var code = apiResult.code;
        var message = apiResult.message;
        var pagingControl = apiResult.pagingControl;
        var longMessage = apiResult.longMessage;
        var token = apiResult.token;
        switch (status) {
          case 'success':
            appService.updateSecurityToken(token);
            getSpottedCars();
            break;
        }
      });


    }

    $scope.clearErrors = function () {
      $scope.showValidation = false;
    };

    $scope.showErrors = function () {
      $scope.showValidation = $scope.editMode == 'new' ? beginShowingErrors : true;
    };

    $scope.getError = function (error) {
      if (angular.isDefined(error)) {
        if (error.required) {
          return "Please enter a value for";
        }
        else {
          return "Please enter a valid ";
        }
      }
    };

    $scope.firstPage = function () {
      if ($scope.page !== 1) {
        $scope.page = 1;
      }
    };

    $scope.nextPage = function () {
      if ($scope.lastPage !== $scope.page) {
        $scope.page++;
      }
    };

    $scope.previousPage = function () {
      if ($scope.page !== 1) {
        $scope.page--;
      }
    }

    $scope.goToLastPage = function () {
      if ($scope.page !== $scope.lastPage) {
        $scope.page = $scope.lastPage;
      }
    }

    $scope.$watchCollection('[state, page, sort, perPage]', function () {
      if (startWatching)
        getSpottedCars();
    });

    railOpsService.getAllTracksForTerminal($scope.terminalId, token).then(function (response) {
      var status = response.status;
      if (status == 'OK') {
        $scope.trackList = response.object;
        writeLog('initializing, successful retrieval of trackList for TerminalId ' + $scope.terminalId + ' ' + $scope.trackList.length + ' tracks retrieved');
        if ($scope.trackList.length > 0) {
          var theTrack = null;
          if ($scope.trackId == 0) {
            theTrack = $scope.trackList[Object.keys($scope.trackList)[0]];
          }
          else {
            theTrack = $scope.trackList[$scope.trackId];
          }
          getTrackNameList($scope.trackList);
          if (theTrack != null) {
            $scope.trackId = theTrack.trackId;
            $scope.selectedTrack = theTrack.trackName;
            localStorageService.set('railOpsTrackId', $scope.trackId);
            writeLog('initializing, storing TrackId ' + $scope.trackId + ' in LS');
            if ($scope.trackId == null) {
              writeLog('initializing, TrackId ' + $scope.trackId + ' not found in list of tracks for TerminalId ' + $scope.terminalId);
              $scope.trackId = 0;
            }
            else {
              writeLog('initializing, TrackId ' + $scope.trackId + ' successfully found for TerminalId ' + $scope.terminalId);
            }
            writeLog('initializing, calling getSpottedCars');
            getSpottedCars();
          }
        }
      }
    });

    var startTimer = function () {
      var timer = $timeout(function () {
        $timeout.cancel(timer);
        getSpottedCars();
      }, 300);
    }


  }]);

function initializeSpottedCars(scope, routeParams) {
  scope.spottedCars = [];
  scope.page = 1;
  scope.perPage = 12;
  scope.query = '';
  scope.firstDisabled = true;
  scope.prevDisabled = true;
  scope.nextDisabled = false;
  scope.lastDisabled = true;
  scope.selectedStatus = 'Active';
  scope.statusList = statusList;
  scope.nextActionList = nextActionList;
};


var locateRailCarModalController = function ($scope, $modalInstance, $http, modalControl, appService, tankCarService, railOpsService) {
  $scope.page = 1;
  $scope.perPage = 10;
  $scope.selectedPrefix = '';
  $scope.selectedTankCar = '';
  $scope.tankCars = [];

  $scope.entityNameLabel = 'Spot Name';
  $scope.entityNamePrompt = 'enter spot name';
  $scope.entityStatusLabel = 'Spot Status';
  $scope.mode = modalControl.mode;
  $scope.entity = {};
  $scope.entity.entityName = $scope.mode == 'new' ? null : modalControl.originalName;
  $scope.entity.selectedStatus = $scope.mode == 'new' ? 'Active' : modalControl.originalStatus;
  $scope.spotId = modalControl.spotId;
  $scope.orgId = modalControl.orgId;
  var startWatching = false;

  $scope.prefixList = "abcd";

  $scope.authenticatedUserId = appService.authentication.authenticatedUserId;
  $scope.authenticatedUserName = appService.authentication.authenticatedUserName;
  var token = appService.authentication.token;

  // get list of tank car prefixes
  var getTankCarPrefixes = function () {
    tankCarService.getTankCarPrefixes(token).then(function (response) {
      var apiResult = response.data;
      var status = apiResult.apiStatus.toLowerCase();
      var code = apiResult.code;
      var message = apiResult.message;
      var pagingControl = apiResult.pagingControl;
      var longMessage = apiResult.longMessage;
      var token = apiResult.token;
      switch (status) {
        case 'success':
          appService.updateSecurityToken(token);
          $scope.prefixList = apiResult.responseData;
          break;
      }
    });
  };

  $scope.onSelectedPrefix = function () {
    $scope.selectedPrefix = this.selectedPrefix;
    getTanksCarsByPrefix($scope.selectedPrefix);
  }

  var getTanksCarsByPrefix = function (selectedPrefix) {
    tankCarService.getTankCarsByPrefix($scope.orgId, selectedPrefix, $scope.page, $scope.perPage, token).then(function (response) {
      var apiResult = response.data;
      var status = apiResult.apiStatus.toLowerCase();
      var code = apiResult.code;
      var message = apiResult.message;
      var pagingControl = apiResult.pagingControl;
      var longMessage = apiResult.longMessage;
      var token = apiResult.token;
      switch (status) {
        case 'success':
          appService.updateSecurityToken(token);
          $scope.tankCars = apiResult.responseData;
          $scope.lastPage = Math.ceil(pagingControl.totalEntityCount / $scope.perPage);
          if ($scope.lastPage == 0) $scope.lastPage = 1;
          $scope.totalTankCars = pagingControl.totalEntityCount;
          $scope.firstDisabled = $scope.page == 1;
          $scope.prevDisabled = $scope.page == 1;
          $scope.nextDisabled = $scope.page == $scope.lastPage;
          $scope.lastDisabled = $scope.page == $scope.lastPage;
          break;
      }
    });
  }

  $scope.selectTankCar = function (tankCarId) {
    var apiInput = {};
    apiInput.apiAction = $scope.editMode == 'new' ? 'add' : 'update';
    apiInput.userId = $scope.authenticatedUserId;
    apiInput.code = 0;
    apiInput.command = '';
    apiInput.value = {};
    apiInput.value.spotId = $scope.spotId;
    apiInput.value.tankCarId = tankCarId;

    railOpsService.spotTankCar(apiInput, token).then(function (response) {
      var apiResult = response.data;
      var status = apiResult.apiStatus.toLowerCase();
      var code = apiResult.code;
      var message = apiResult.message;
      var pagingControl = apiResult.pagingControl;
      var longMessage = apiResult.longMessage;
      var token = apiResult.token;
      switch (status) {
        case 'success':
          appService.updateSecurityToken(token);
          break;
      }
    });

    $modalInstance.close(null); // don't return anything
  }

  $scope.cancel = function () {
    $modalInstance.dismiss('cancel');
  };


  $scope.firstPage = function () {
    if ($scope.page !== 1) {
      $scope.page = 1;
    }
  };

  $scope.nextPage = function () {
    if ($scope.lastPage !== $scope.page) {
      $scope.page++;
    }
  };

  $scope.previousPage = function () {
    if ($scope.page !== 1) {
      $scope.page--;
    }
  }

  $scope.goToLastPage = function () {
    if ($scope.page !== $scope.lastPage) {
      $scope.page = $scope.lastPage;
    }
  }

  $scope.$watchCollection('[page, perPage]', function () {
    if (startWatching) {
      if ($scope.selectedPrefix != '') {
        getTanksCarsByPrefix($scope.selectedPrefix);
      }
    }
  });

  getTankCarPrefixes();

  startWatching = true;

};


