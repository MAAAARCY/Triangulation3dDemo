mergeInto(LibraryManager.library, {
    Callback: function(message) {
        window.dispatchReactUnityEvent(
            "Callback",
            Pointer_stringify(message)
        );
    }
}); 