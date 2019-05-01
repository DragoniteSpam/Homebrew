# Building instructions

We used Unity 2018.1.3f to make this, using the "Build" button and setting the target as "Windows" (x86_64) seems to work

Preferably make the starting scene the title screen one, but anything that isn't the scene named Don't Touch should work, because Don't Touch doesn't have some of the helper game objects/scripts like audio and whatever that the rest of the game likes to reference

Realistically it should work with any modern version of Unity since nothing relies on any crazy quirk of the engine, but if something breaks it's probably going to be cinemachine, because that's the part that I have the least of an idea how to fix

If you're adventurous enough to try the HTML5 target let me know how it goes

Be careful with Unity Cloud, especially if you pulled this down from github, because it likes to break