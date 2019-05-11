	DECLARE @selection INT
	DECLARE @iterator INT
	DECLARE @numbooks INT
	DECLARE @sortingTable TABLE
	(
		book_id int,
		totalPoints int
	)

	INSERT INTO @sortingTable (book_id)
		SELECT dbo.bookSelection.bookID 
			FROM dbo.bookSelection
	
	UPDATE @sortingTable
		SET totalPoints = 0

	SELECT @numbooks = COUNT(bookID) FROM dbo.bookSelection
	SET @iterator = 1

	WHILE @iterator <= @numbooks
	BEGIN
		SELECT @selection = COUNT(*) 
		FROM dbo.annualVoteSubmission WHERE votePreference1 = @iterator
		GROUP BY votePreference1
		HAVING COUNT(*) > 0

		IF @@ROWCOUNT > 0
		BEGIN
			UPDATE @sortingTable
				SET totalPoints = totalPoints + (@selection * 7)
				WHERE book_id = @iterator
		END

		SELECT @selection = COUNT(*) 
		FROM dbo.annualVoteSubmission WHERE votePreference2 = @iterator
		GROUP BY votePreference2
		HAVING COUNT(*) > 0

		IF @@ROWCOUNT > 0
		BEGIN
			UPDATE @sortingTable
				SET totalPoints = totalPoints + (@selection * 6)
				WHERE book_id = @iterator
		END

		SELECT @selection = COUNT(*) 
		FROM dbo.annualVoteSubmission WHERE votePreference3 = @iterator
		GROUP BY votePreference3
		HAVING COUNT(*) > 0

		IF @@ROWCOUNT > 0
		BEGIN
			UPDATE @sortingTable
				SET totalPoints = totalPoints + (@selection * 5)
				WHERE book_id = @iterator
		END

		SELECT @selection = COUNT(*) 
		FROM dbo.annualVoteSubmission WHERE votePreference4 = @iterator
		GROUP BY votePreference4
		HAVING COUNT(*) > 0

		IF @@ROWCOUNT > 0
		BEGIN
			UPDATE @sortingTable
				SET totalPoints = totalPoints + (@selection * 4)
				WHERE book_id = @iterator
		END

		SELECT @selection = COUNT(*) 
		FROM dbo.annualVoteSubmission WHERE votePreference5 = @iterator
		GROUP BY votePreference5
		HAVING COUNT(*) > 0

		IF @@ROWCOUNT > 0
		BEGIN
			UPDATE @sortingTable
				SET totalPoints = totalPoints + (@selection * 3)
				WHERE book_id = @iterator
		END

		SELECT @selection = COUNT(*) 
		FROM dbo.annualVoteSubmission WHERE votePreference6 = @iterator
		GROUP BY votePreference6
		HAVING COUNT(*) > 0

		IF @@ROWCOUNT > 0
		BEGIN
			UPDATE @sortingTable
				SET totalPoints = totalPoints + (@selection * 2)
				WHERE book_id = @iterator
		END

		SELECT @selection = COUNT(*) 
		FROM dbo.annualVoteSubmission WHERE votePreference7 = @iterator
		GROUP BY votePreference7
		HAVING COUNT(*) > 0

		IF @@ROWCOUNT > 0
		BEGIN
			UPDATE @sortingTable
				SET totalPoints = totalPoints + (@selection * 1)
				WHERE book_id = @iterator
		END

		SET @iterator = @iterator + 1
	END

SELECT book_id, totalPoints, bookSelection.bookTitle, bookselection.bookAuthor FROM @sortingTable 
INNER JOIN dbo.bookSelection ON book_id = dbo.bookSelection.bookID
ORDER BY totalPoints DESC, book_id
