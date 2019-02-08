function printContent1(el) {
	var restorepage = document.body.innerHTML;
	var printcontent = document.getElementById(el).innerHTML;
	document.body.innerHTML = printcontent;
	window.print();
	document.body.innerHTML = restorepage;
	//location.reload();
	//location.reload(true);
	return false;
}
function printContent2(el) {
	var restorepage = document.body.innerHTML;
	var printcontent = document.getElementById(el).innerHTML;
	document.body.innerHTML = printcontent;
	window.print();
	document.body.innerHTML = restorepage;
	//location.reload();
	location.reload(true);
	return false;
}
function printContent(el) {
	var win = window.open();
	//var win = window.open('','','left=0,top=0,width=552,height=477,toolbar=0,scrollbars=0,status =0');

	var content = '<html>';
	content += '<head>';
	//content += '<link rel="stylesheet" href="../Content/plugins/font-awesome-4.7.0/css/font-awesome.css">';

content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/animate.css">';
content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/backgrounds.css">';
content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/boilerplate.css">';
content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/border-radius.css">';     
content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/grid.css">';
content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/page-transitions.css">';
content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/spacing.css">';
content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/typography.css">';
content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/utils.css">';
content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/colors.css">';     
                                                                
                                                                
content += '<link rel="stylesheet" type="text/css" href="../assets/themes/admin/layout.css">';
content += '<link rel="stylesheet" type="text/css" href="../assets/themes/admin/color-schemes/default.css">';
content += '<link rel="stylesheet" type="text/css" href="../assets/themes/components/default.css">';
content += '<link rel="stylesheet" type="text/css" href="../assets/themes/components/border-radius.css">';
content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/responsive-elements.css">';
content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/admin-responsive.css">';     
                                                                                                                    
	content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/animate.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/backgrounds.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/boilerplate.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/border-radius.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/grid.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/page-transitions.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/spacing.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/typography.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/utils.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/badges.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/buttons.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/content-box.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/forms.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/images.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/info-box.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/invoice.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/loading-indicators.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/panel-box.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/response-messages.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/responsive-tables.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/ribbon.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/social-box.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/tables.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/tile-box.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/elements/timeline.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/frontend-elements/blog.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/frontend-elements/cta-box.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/frontend-elements/feature-box.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/frontend-elements/footer.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/frontend-elements/hero-box.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/frontend-elements/icon-box.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/frontend-elements/portfolio-navigation.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/frontend-elements/pricing-table.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/frontend-elements/sliders.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/frontend-elements/testimonial-box.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/icons/linecons/linecons.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/icons/spinnericon/spinnericon.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/fullpage/fullpage.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/themes/componentsfontend/border-radius.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/themes/frontend/layout.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/responsive-elements.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/helpers/frontend-responsive.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/accordion-ui/accordion.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/layerslider/layerslider.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/owlcarousel/owlcarousel.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/nestable/nestable.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/noty-notifications/noty.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/popover/popover.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/pretty-photo/prettyphoto.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/progressbar/progressbar.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/range-slider/rangeslider.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/slider-ui/slider.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/summernote-wysiwyg/summernote-wysiwyg.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/tabs-ui/tabs.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/theme-switcher/themeswitcher.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/timepicker/timepicker.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/tocify/tocify.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/tooltip/tooltip.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/touchspin/touchspin.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/uniform/uniform.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/wizard/wizard.css">';
	content += '<link rel="stylesheet" type="text/css" href="../assets/widgets/xeditable/xeditable.css">';
	content += '';
	content += '';
	content += '</head >';
	//content += '<body>';
	content += '<body onload=\"window.print(); window.close();\">';
	content += document.getElementById(el).innerHTML;
	content += '</body>';
	content += '</html>';
	win.document.write(content);
	//win.print();
	win.focus();
	win.document.close();
	//win.close();
	
}
function printContent4(el) {
	//var win = window.open();
	var win = window.open('', '', 'left=0,top=0,width=552,height=477,toolbar=0,scrollbars=0,status =0');
	var content = "<html>";
	content += "<body onload=\"window.print(); window.close();\">";
	content += document.getElementById(el).innerHTML;
	content += "</body>";
	content += "</html>";
	win.document.write(printcontent);
	win.document.close();
}

//function printContent1(el) {
//	var restorepage = document.body.innerHTML;
//	var printcontent = document.getElementById(el).innerHTML;
//	document.body.innerHTML = printcontent;
//	window.print();
//	document.body.innerHTML = restorepage;
//	//location.reload();
//	//location.reload(true);
//	return false;
//}
//function printContent2(el) {
//	var restorepage = document.body.innerHTML;
//	var printcontent = document.getElementById(el).innerHTML;
//	document.body.innerHTML = printcontent;
//	window.print();
//	document.body.innerHTML = restorepage;
//	//location.reload();
//	location.reload(true);
//	return false;
//}
//function printContent3(el) {
//	var win = window.open();
//	//var win = window.open('','','left=0,top=0,width=552,height=477,toolbar=0,scrollbars=0,status =0');

//	var content = '<html>';
//	content += '<head>';
//	content += '<link rel="stylesheet" href="@Url.Content("../Content/plugins/font-awesome-4.7.0/css/font-awesome.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../Content/Font/FontSizeNormal.css")">';
//	content += ' <link rel="stylesheet" type="text/css" href="@Url.Content("../Content/Font/DesignMore.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../Content/Font/StyleDesign.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../Content/Font/ColorNormal.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/helpers/animate.css")">';
//	content += '    <link rel="stylesheet" type="text/css" href="@Url.Content("../assets/helpers/backgrounds.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/helpers/backgrounds.css")">';
//	content += ' <link rel="stylesheet" type="text/css" href="@Url.Content("../assets/helpers/boilerplate.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/helpers/border-radius.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/helpers/grid.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/helpers/page-transitions.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/helpers/spacing.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/helpers/typography.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/helpers/utils.css")">';
//	content += ' <link rel="stylesheet" type="text/css" href="@Url.Content("../assets/elements/badges.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/elements/buttons.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/elements/content-box.css")">';
//	content += ' <link rel="stylesheet" type="text/css" href="@Url.Content("../assets/elements/forms.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/elements/images.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/elements/info-box.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/elements/invoice.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/elements/loading-indicators.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/elements/panel-box.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/elements/response-messages.css")">';
//	content += '';
//	content += ' <link rel="stylesheet" type="text/css" href="@Url.Content("../assets/widgets/fullpage/fullpage.css")">';
//	content += '   <link rel="stylesheet" type="text/css" href="@Url.Content("../assets/themes/componentsfontend/border-radius.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/themes/frontend/layout.css")">';
//	content += ' <link rel="stylesheet" type="text/css" href="@Url.Content("../assets/helpers/responsive-elements.css")">';
//	content += '<link rel="stylesheet" type="text/css" href="@Url.Content("../assets/helpers/frontend-responsive.css")">';
//	content += '</head >';
//	content += '<body onload=\"window.print(); window.close();\">';
//	content += document.getElementById(el).innerHTML;
//	content += '<script type="text/javascript" src="@Url.Content("../assets/bootstrap/js/bootstrap.js")">';
//	content += '</body>';
//	content += '</html>';
//	win.document.write(content);
//	win.print();

//	//win.document.close();
//}
//function printContent4(el) {
//	//var win = window.open();
//	var win = window.open('', '', 'left=0,top=0,width=552,height=477,toolbar=0,scrollbars=0,status =0');

//	var content = "<html>";
//	content += "<body onload=\"window.print(); window.close();\">";
//	content += document.getElementById(el).innerHTML;
//	content += "</body>";
//	content += "</html>";
//	win.document.write(printcontent);
//	win.document.close();
//}