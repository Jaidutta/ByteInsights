let index = 0;

function AddTag() {
    // Get Reference to the TagEntry input element -- where the user types in the tags
    var tagEntry = document.getElementById("TagEntry");

    // Lets use the new search function to help detect an error state
    let searchResult = search(tagEntry.value);
    if (searchResult != null) {
        // Trigger sweet alert for whatever condition is contained in the search results var
        swalWithDarkButton.fire({
            html: `<span class='font-weight-bolder'>${searchResult.toUpperCase()}</span>`
        });
    }
    else {
        // Create a new Select Option
        // first argument is the text and the 2nd argument is the value
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
    }

    // Clear out the TagEntry control
    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    /*
     The selectedIndex property sets or returns the index of the selected option in a drop-down list.

     The index starts at 0.

     Note: If the drop-down list allows multiple selections it will only return the index of the first option selected.

     Note: The value "-1" will deselect all options (if any).

     Note: If no option is selected, the selectedIndex property will return -1.
     */

    let tagList = document.getElementById("TagList");
    
    if (!tagList) return false;

    if (tagList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: "<span class='font-weight-bolder'>CHOOSE A TAG BEFORE DELETING</span>"
        });
        return true;
    }

    console.log("Debug: tagList not null")

    let tagCount = 1;
    
    while (tagCount > 0) {
        
        let selectedIndex = tagList.selectedIndex;
        if (selectedIndex >= 0) {
            tagList.options[selectedIndex] = null;
            --tagCount;
        }
        else 
            tagCount = 0;

        index--;
        
    }
}

/* When the form gets submitted, go and find the TagList id, all of the option
   interact with the selected property, making them all selected.
   So in short, it will select all the options within the select list and 
   will pass all the values when the form gets submitted
*/
$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})

// Look for the tagValues variable to see if it has data
if (tagValues != '') {
    let tagArray = tagValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        // Load up or Replace the options that we have

        ReplaceTag(tagArray[loop], loop);
        index++;
    }
}

function ReplaceTag(tag, index) {
    // new Option(text, value)
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}


// The Search function will detect either an empty of a duplicate Tag
// and return and error string if an error is detected

function search(str) {
    if (str === "") {
        return "Empty Tags are not permitted";
    }

    var tagsEl = document.getElementById('TagList');

    if (tagsEl) {
        let options = tagsEl.options;
        for (let index = 0; index < options.length; index++) {
            if (options[index].value === str) {
                return `The Tag #${str} was detected as a duplicate and not permitted.`;
            }
        }
    }
}

const swalWithDarkButton = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-danger btn-sm w-100 btn-outline-dark'
    },
    imageUrl: '/images/HoldOnAlert.jpg',
    timer: 3000,
    buttonsStyling: false
});