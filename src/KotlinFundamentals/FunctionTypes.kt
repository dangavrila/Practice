fun main() {
    val cupcake: (Int) -> String = {
        "Have a cupcake!"
    }

    val treatFunc = trickOrTreat(false) { "$it quarters" }
    val trickFunc = trickOrTreat(true, null)
    repeat(4) {
        treatFunc()
    }
    trickFunc()
}

val trick = {
    println("No treats!")
}

val treat: () -> Unit = {
    println("Have a treat!")
}

fun trickOrTreat(isTrick: Boolean, extraTreat: ((Int) -> String)?): () -> Unit {
    if (isTrick){
        return trick
    }
    else {
        println(extraTreat?.invoke(5))
        return treat
    }
}