/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';

    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.languages = 'vi';
	config.filebrowserBrowseUrl = '/BeStrong/assetsBackend/ckfinder/ckfinder.html';
	config.filebrowserImageBrowseUrl = '/BeStrong/assetsBackend/ckfinder/ckfinder.html?Types=Images';
	config.filebrowserFlashBrowseUrl = '/BeStrong/assetsBackend/ckfinder/ckfinder.html?Types=Flash';
	config.filebrowserUploadUrl = '/BeStrong/assetsBackend/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=File';
	config.filebrowserImageUploadUrl = '/BeStrong/assetsBackend/Data';
	config.filebrowserFlashUploadUrl = '/BeStrong/assetsBackend/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

	CKFinder.setupCKEditor(null, '/BeStrong/assetsBackend/ckfinder/');
};
