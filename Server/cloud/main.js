
// Use Parse.Cloud.define to define as many cloud functions as you want.
// For example:
Parse.Cloud.define("hello", function(request, response) {
	response.success("Hello shay1!");
});


Parse.Cloud.define("Logger", function(request, response) {
	console.log(request.params);
	response.success();
});

Parse.Cloud.define("GetSuggestions", function(request, response) {
	var firstContent = {
		text: "0: ABCD EFG HIJK LMNOP QRST WXYZ\n" +
			"1: ABCD EFG HIJK LMNOP QRST WXYZ\n" +
			"2: ABCD EFG HIJK LMNOP QRST WXYZ\n" +
			"3: ABCD EFG HIJK LMNOP QRST WXYZ\n",
		articleName: "Seahawks_Win_Superbowl.txt",
		url:"http://www.nfl.com/news/story/0ap3000000497443/article/michael-vick-sells-himself-to-nfl-general-managers",
		pics:"NFLImage1"
	}
	
	var secondContent = {
		text: "Second Content Text",
		url:"http://www.nfl.com/news/story/0ap3000000497443/article/michael-vick-sells-himself-to-nfl-general-managers",
		pics:"NFLImage1"
	}
	
	var suggestions = {
	    first: firstContent,
	    second: secondContent
	}
	response.success(suggestions);
});