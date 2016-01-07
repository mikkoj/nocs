##  ##
# Nocs - a notepad for Google Docs #

Nocs is a C#/Windows Forms project that utilizes the Google Document List API for synchronizing text documents with Google Docs.

<br>
<h2>Nocs 2.3</h2>

<ul><li>Fixed the bug where entries were failing to load due to SSL issues.</li></ul>

<b>Download here:</b> <a href='http://nocs.googlecode.com/files/Nocs.2.3.msi'>http://nocs.googlecode.com/files/Nocs.2.3.msi</a>

<br>
<h2>Nocs 2.2 beta</h2>

<b>Changes:</b>

<ul><li>Added experimental support for both new and old style Google Docs documents<br>
</li><li>Added the ability to pin documents to auto-load them when starting Nocs<br>
</li><li>Text files can be used as parameters so they can be loaded immediately<br>
</li><li>Bugfixes here and there</li></ul>

<br>
<h2>Nocs 2.1</h2>

<b>Features:</b>

<ul><li>added folder support<br>- while browsing: create, delete or rename folders and drag & drop documents to/from folders<br>(use right-click on a selected item to drag)<br>- in preferences: default save folder<br>- while saving: save to any folder<br>
</li><li>added proxy support<br>
</li><li>improved sync by implementing Google's diff-match-patch library:<br><a href='http://code.google.com/p/google-diff-match-patch/'>http://code.google.com/p/google-diff-match-patch/</a>
</li><li>both Google and proxy passwords now encrypted when saved in isolated storage<br>
</li><li>insert mode now tracked with a block-caret<br>
</li><li>added context menu for the text editor area</li></ul>

<b>Fixes:</b>

<ul><li>improved saving in general; Nocs should now handle even the known issues with the Docs API in case they appear<br>
</li><li>when Browse was open and a document was renamed/deleted, listBox wouldn't refresh correctly<br>
</li><li>fixed an invoke error that occurred after long uptimes<br>
</li><li>ctrl + arrow keys weren't the best choice of shortcuts for switching between tabs, now alt + tab (later bindable)<br>
</li><li>bunch of UI tweaks + minor bug fixes</li></ul>

<img src='http://nocs.googlecode.com/files/nocs_2.1.png' />
<br><br>
<h2>Follow me at <a href='http://twitter.com/mikkoj'>http://twitter.com/mikkoj</a> for updates, and please feel free to send suggestions/bugs!</h2>


<hr />

<h3>Nocs 2.0</h3>
<ul><li>Full synchronization with multiple documents open at the same time (tabs)<br>
</li><li>Documents are now saved as actual Google Docs documents, no more Spreadsheet!<br>
</li><li>Because documents can be shared through Google Docs, you can collaborate with others through Nocs as well.<br>Just share a document with a friend and it should show up on his/her list after a restart or in a few minutes.<br>
</li><li>You can now sort documents by title or by time updated.<br>
</li><li>Added three options for the status bar: <b>Text with color</b>, <b>black & white</b> and <b>minimal</b>.<br>
</li><li>Improved regular expression search/replace.</li></ul>

<ul><li><b>Tip:</b> You can close tabs with mouse3 (usually the scroll-button).