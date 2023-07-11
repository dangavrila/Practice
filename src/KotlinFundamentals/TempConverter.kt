fun main() {
    val initialMeasurement = 10.0
    printFinalTemperature(initialMeasurement, "Celsius", "Fahrenheit") { 9/5.0 * it + 32.0 }
    printFinalTemperature(initialMeasurement, "Kelvin", "Celsius") { it - 273.15 }
    printFinalTemperature(initialMeasurement, "Fahrenheit", "Kelvin") { 5/9.0 * (it - 32) + 273.15 }
}

fun printFinalTemperature(
    initialMeasurement: Double, 
    initialUnit: String, 
    finalUnit: String, 
    conversionFormula: (Double) -> Double
) {
    val finalMeasurement = String.format("%.2f", conversionFormula(initialMeasurement)) // two decimal places
    println("$initialMeasurement degrees $initialUnit is $finalMeasurement degrees $finalUnit.")
}
