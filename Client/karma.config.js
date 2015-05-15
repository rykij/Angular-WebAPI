// Karma configuration
// Generated on Fri Apr 24 2015 14:17:18 GMT+0200 (W. Europe Daylight Time)

module.exports = function(config) {
  config.set({

    //singleRun = true,
	/*reporters = ['dots', 'junit'];
	junitReporter = {
		outputFile: 'AngularTest_Results.xml'
	};*/
  
    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: '',

    plugins:['karma-jasmine',
	         'karma-phantomjs-launcher', 
			 'karma-junit-reporter'],
	
	
    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: ['jasmine'],


    // list of files / patterns to load in the browser
    files: [
	  'assets/js/angular/angular.js',
	  'assets/js/angular/angular-mocks.js',
	  'assets/js/angular/angular-ui-router.js',
	   'assets/js/angular/angular-local-storage.js',
	  
	  'app/*module.js',
	  'app/app.config.js',
	  'app/web.config.js',
	  
	  'app/components/**/*Module.js',
	  'app/components/**/*.js',
	  
	  'app/shared/**/*Module.js',
	  'app/shared/**/*.js',
      	  
	  
	  
      'app_test/unit/**/*.js'
    ],


    // list of files to exclude
    exclude: [
    ],


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: {
    },


    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['progress', 'junit'],

    junitReporter: {
       outputFile: '../unitTestResults.xml'
    },
	
    hostname: 'localhost',
	
    // web server port
    port: 16129,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: false,


     // start these browsers
    // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
    browsers: ['PhantomJS'],


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: true
  });
};
