def greeting():
    print("GMJ2Book")
    print("Сделает книгу в формате FB2 из блога на портале My.GMJ")
    print("(c) 2017 Nikita Kovin")


def indent(message, newline=True):
    if newline is True:
        print("\t", message)
    else:
        print("\t", message, end=" ")


def line(length=90):
    i = 0
    while i < length:
        print("=", end='')
        i += 1
    print()
