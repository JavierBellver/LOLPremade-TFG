/// <binding BeforeBuild='min' Clean='clean' ProjectOpened='watch' />
"use strict";

var gulp = require("gulp"),
    browserify = require("browserify"),
    babelify = require("babelify"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    htmlmin = require("gulp-htmlmin"),
    uglify = require("gulp-uglify"),
    merge = require("merge-stream"),
    del = require("del"),
    bundleconfig = require("./bundleconfig.json"),
    source = require('vinyl-source-stream'),
    buffer = require('vinyl-buffer'),
    sourcemaps = require('gulp-sourcemaps')

var regex = {
    css: /\.css$/,
    html: /\.(html|htm)$/,
    js: /\.js$/,
    jsx: /\.jsx$/
};

gulp.task("min", ["min:js", "min:css", "min:html", "min:landingpagejsx", "min:mainpagejsx"]);

gulp.task("min:js", function () {
    var tasks = getBundles(regex.js).map(function (bundle) {
        return gulp.src(bundle.inputFiles, { base: "." })
            .pipe(concat(bundle.outputFileName))
            .pipe(uglify())
            .pipe(gulp.dest("."));
    });
    return merge(tasks);
});

gulp.task("min:landingpagejsx", function () {
    return browserify("wwwroot/js/react-components/landingPage/landingPage.jsx", { debug: true, extensions: ['es6'] })
        .transform("babelify", { presets: ["es2015", "react"] })
        .bundle()
        .pipe(source('landingPage.jsx'))
        .pipe(buffer())
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('wwwroot/app'));
});

gulp.task("min:mainpagejsx", function () {
    return browserify("wwwroot/js/react-components/mainPage/mainPage.jsx", { debug: true, extensions: ['es6'] })
        .transform("babelify", { presets: ["es2015", "react"] })
        .bundle()
        .pipe(source('mainPage.jsx'))
        .pipe(buffer())
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('wwwroot/app'));
});

gulp.task("min:css", function () {
    var tasks = getBundles(regex.css).map(function (bundle) {
        return gulp.src(bundle.inputFiles, { base: "." })
            .pipe(concat(bundle.outputFileName))
            .pipe(cssmin())
            .pipe(gulp.dest("."));
    });
    return merge(tasks);
});

gulp.task("min:html", function () {
    var tasks = getBundles(regex.html).map(function (bundle) {
        return gulp.src(bundle.inputFiles, { base: "." })
            .pipe(concat(bundle.outputFileName))
            .pipe(htmlmin({ collapseWhitespace: true, minifyCSS: true, minifyJS: true }))
            .pipe(gulp.dest("."));
    });
    return merge(tasks);
});

gulp.task("clean", function () {
    var files = bundleconfig.map(function (bundle) {
        return bundle.outputFileName;
    });
    return del(files);
});

gulp.task("watch", function () {
    getBundles(regex.js).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:js"]);
    });

    getBundles(regex.jsx).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:landingpagejsx"]);
    });

    getBundles(regex.jsx).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:mainpagejsx"]);
    });

    getBundles(regex.css).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:css"]);
    });

    getBundles(regex.html).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:html"]);
    });
});

function getBundles(regexPattern) {
    return bundleconfig.filter(function (bundle) {
        return regexPattern.test(bundle.outputFileName);
    });
}