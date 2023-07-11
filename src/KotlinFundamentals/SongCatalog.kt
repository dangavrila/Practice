class Song constructor(val title: String, val artist: String, val year: Int, val playCount: Long) {
    val isPopular: Boolean = if (playCount > 1000L) true else false
    
    fun printDesc() : Unit {
        println("$title, performed by $artist, was realeased in $year.")
    }
}

fun main() {
	val song:Song = Song(title = "November Rain", artist = "Guns'n Roses", year = 1991, playCount = 1256000000L)
    song.printDesc()
}