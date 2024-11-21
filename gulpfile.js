var gulp = require('gulp');
const sass = require('gulp-sass')(require('sass'));
postcss = require("gulp-postcss");
autoprefixer = require("autoprefixer");
var sourcemaps = require('gulp-sourcemaps'); 
var browserSync = require('browser-sync').create(); 
cssbeautify = require('gulp-cssbeautify');
var beautify = require('gulp-beautify');


//_______ task for scss folder to css main style 
gulp.task('watch', function() {
	console.log('Command executed successfully compiling SCSS in assets.');
	 return gulp.src('wwwroot/assets/scss/**/*.scss') 
		.pipe(sourcemaps.init())                       
		.pipe(sass())
		.pipe(sourcemaps.write(''))  
		.pipe(beautify.css({ indent_size: 4 })) 
		.pipe(gulp.dest('wwwroot/assets/css'))
		.pipe(browserSync.reload({
		  stream: true
	}))
})
 
 //_______task for skin-modes
gulp.task('skins', function(){
	return gulp.src('wwwroot/assets/css/skin-modes.scss')
		 .pipe(sourcemaps.init())
		 .pipe(sass())
		 .pipe(beautify.css({ indent_size: 4 }))
		 .pipe(sourcemaps.write('.'))
		 .pipe(gulp.dest('wwwroot/assets/css/'));
 });


const uglify = require('gulp-uglify');
gulp.task('minify-js', function () {
	return gulp.src('wwwroot/assets/js/Ensamble/Ensamble.js')
		.pipe(uglify({
			mangle: {
				toplevel: true  // Cambia los nombres de las variables a nivel global
			},
			compress: {
				drop_console: true  // Elimina los `console.log()` para mejorar la seguridad
			},
			output: {
				comments: false  // Elimina los comentarios
			}
		}))
		.pipe(gulp.dest('wwwroot/js/dist'));
});



gulp.task('default', gulp.series('minify-js'));